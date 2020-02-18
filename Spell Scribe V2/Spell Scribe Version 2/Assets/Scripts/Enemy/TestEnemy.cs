using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public override void EnemyAttack()
    {
        //Insert attack logic and attack animations

        player.TakeDamage(damage);
        Invoke("EndEnemyTurn", 1);

    }
}
