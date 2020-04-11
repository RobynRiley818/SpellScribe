using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public Slider healthBar;
    protected Player player;
    TurnManager turnManager;
    public int damage = 1;

    SoundEffects soundEffects;

    EnemyModifier[] currentModdifiers;
    [HideInInspector] public bool stunned;
    private void Start()
    {
        stunned = false;
        soundEffects = FindObjectOfType<SoundEffects>();
        healthBar.maxValue = health;
        healthBar.value = health;
        player = FindObjectOfType<Player>();
        turnManager = FindObjectOfType<TurnManager>();
    }

    public void StartEnemyTurn()
    {
        currentModdifiers = FindObjectsOfType<EnemyModifier>();
        StartCoroutine(Mods());
    }

    IEnumerator Mods()
    {
        yield return new WaitForSeconds(1);
        foreach (EnemyModifier em in currentModdifiers)
        {
            em.StartOfTurnEffect();

            yield return new WaitForSeconds(1);
        }

        if (!stunned)
        {
            EnemyAttack();
            StateManager.currentState = StateManager.GameState.EnemyTurn;
        }

        else
        {
            Debug.Log("The Enemy Skips there turn");
            EndEnemyTurn();
            stunned = false;
        }
   
    }

    public abstract void EnemyAttack();

    public void TakeDamage(int dam)
    {
        health -= dam;
        healthBar.value = health;
        soundEffects.PlaySound("dewHurt");
        if (health <= 0)
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
