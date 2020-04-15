using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : EnemyModifier
{
    private int maxStunAmmount = 5;


    new private void Start()
    {
        base.Start();
        modHolder.number.text = "" + effectNum;

    }

    public override string GetDescription()
    {
        description = "When the enemy's stun ammount is greater than " + maxStunAmmount + " the enemy will skip there turn";
        return description;
    }

    public override void InitialEffect()
    {
        Debug.Log("No Initial Effect");
    }

    public override void StartOfTurnEffect()
    {
        if(effectNum >= maxStunAmmount)
        {
            StunEffect();
            ChangeStunAmmount(-maxStunAmmount);

            if (effectNum <= 0)
            {
                Destroy(this.gameObject);
            }

        }
    }

    public void ChangeStunAmmount(int change)
    {
        effectNum += change;
        modHolder.number.text = "" + effectNum;
    }

    private void StunEffect()
    {
        thisEnemy.stunned = true;
    }
}
