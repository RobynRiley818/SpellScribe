using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Burning : EnemyModifier
{
    public ParticleSystem buring;
    new private void Start()
    {
        base.Start();
        modHolder.number.text = "" + effectNum;

    }
    public override void StartOfTurnEffect()
    {
        var temp = Instantiate(buring, thisEnemy.transform.parent.transform);
        temp.transform.position = this.transform.position;

        Invoke("Damage", .1f);
    }

    private void Damage()
    {
        StateManager.currentState = StateManager.GameState.EnemyHit;
        modHolder = this.gameObject.transform.parent.gameObject.transform.GetComponent<ModifierHolder>();
        thisEnemy.TakeDamage(effectNum);
        ChangeBurnAmmount(-1);
        Debug.Log("Burn Is Being Called");
        if (effectNum <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void ChangeBurnAmmount(int change) {
        effectNum += change;
        modHolder.number.text = "" + effectNum;
    }
    public int GetBurnDamage() { return effectNum; }

    public override void InitialEffect() { }

    public override string GetDescription()
    {
        description = "Enemy Takes Burn Damage Every Turn Equal to <color=red>" + effectNum + "</color> Burn ammount decreaes by 1 every turn";
        return description;
    }
}