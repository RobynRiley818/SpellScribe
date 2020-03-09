using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    private int maxHealth;
    public Slider healthBar;
    private GenerateSpellCards spellCards;

    public GameObject losePanel;
    Deck deck;

    public Text healhText;
    private ScreenShake screenShake;
    private ScreenFlash screenFlash;

    private SoundEffects sound;
    private void Start()
    {
        sound = FindObjectOfType<SoundEffects>();
        deck = FindObjectOfType<Deck>();
        deck.ResetDecks();
        healthBar.maxValue = health;
        healthBar.value = health;

        maxHealth = health;
        spellCards = FindObjectOfType<GenerateSpellCards>();
        healhText.text = "<b>Health </b>" + health + " / " + maxHealth;
        screenShake = FindObjectOfType<ScreenShake>();
        screenFlash = FindObjectOfType<ScreenFlash>();
    }

    public void TakeDamage(int dam)
    {
        health -= dam;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.value = health;
        if (health <= 0)
        {
            PlayerDied();
        }

        if(dam > 0)
        {
            screenFlash.StartCoroutine("Flash");
            screenShake.TriggerShake();
            sound.PlaySound("hurt");
        }
       
        healhText.text = "<b>Health </b>" + health + " / " + maxHealth;
    }

    private void PlayerDied()
    {
        losePanel.SetActive(true);
        Destroy(this.gameObject);
    }

    public void StartPlayerTurn()
    {
        spellCards = FindObjectOfType<GenerateSpellCards>();
        if (spellCards != null)
        {
            if (deck.newHand.Count == 0)
            {
                spellCards.Draw();
            }

            else
            {
                spellCards.TurnOnhand();
            }

        }

    }
}
