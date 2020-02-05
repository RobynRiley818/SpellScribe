using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnAttack : MonoBehaviour
{
    //Amount of Health attack takes away
    public int damage;

    //Chance of attack being used each turn
    public int chance;

    public EnAttack()
    {
        damage = 0;
        chance = 0;
    }

    public EnAttack(int dmg, int chn)
    {
        damage = dmg;
        chance = chn;
    }
}


public class Enemy : MonoBehaviour
{
    public int health;

    List<EnAttack> attacks;


    /*Example Enemy Attack Below 

    EnAttack ClawSwipe = new EnAttack(5, 15);
    */
}
