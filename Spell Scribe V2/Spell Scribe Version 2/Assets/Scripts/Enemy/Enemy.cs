using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


public abstract class Enemy : MonoBehaviour
{
    public int health;
    public Slider healthBar;
    protected Player player;
    TurnManager turnManager;

    private void Start()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        player = FindObjectOfType<Player>();
        turnManager = FindObjectOfType<TurnManager>();
    }

    public void StartEnemyTurn()
    {
        Invoke("EnemyAttack", 1);
    }

    public abstract void EnemyAttack();

    public void TakeDamage(int dam)
    {
        health -= dam;
        healthBar.value = health;
        if(health <= 0)
        {
            EnemyDies();
        }
    }

    private void EnemyDies()
    {
        turnManager.PlayerWon();
        Destroy(this.gameObject);
    }

    public void EndEnemyTurn()
    {
        StateManager.currentState = StateManager.GameState.SpellSelect;
        player.StartPlayerTurn();
    }
}
