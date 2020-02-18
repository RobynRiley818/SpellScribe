using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public override void EnemyAttack()
    {
        //Insert attack logic and attack animations

        player.TakeDamage(1);
        Invoke("EndEnemyTurn", 1);

    }
}
