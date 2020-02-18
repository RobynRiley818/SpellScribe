using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    public Slider healthBar;
    private GenerateSpellCards spellCards;

   public GameObject losePanel;

    private void Start()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        spellCards = FindObjectOfType<GenerateSpellCards>();
    }

    public void TakeDamage(int dam)
    {
        health -= dam;
        healthBar.value = health;
        if (health <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        losePanel.SetActive(true);
        Destroy(this.gameObject);
    }

    public void StartPlayerTurn()
    {
        Debug.Log("Player Turn Starting");
        spellCards = FindObjectOfType<GenerateSpellCards>();
        if(spellCards != null)
        {
            spellCards.Draw();
        }

    }
}
