using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public override void EnemyAttack()
    {
        player.TakeDamage(damage);
        Invoke("EndEnemyTurn", 1);
    }

    private void Heal()
    {
        TakeDamage(-3);
    }

    private void BasicAttack()
    {
        player.TakeDamage(damage);
    }
}
