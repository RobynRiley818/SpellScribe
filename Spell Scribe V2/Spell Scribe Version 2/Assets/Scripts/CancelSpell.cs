using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelSpell : MonoBehaviour
{
    SpellManager spellManager;
    GenerateWriting generateWriting;

    private void Start()
    {
        spellManager = FindObjectOfType<SpellManager>();
        generateWriting = FindObjectOfType<GenerateWriting>();
        this.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        foreach(BaseCard card in spellManager.cards)
        {
            if (!card.inDiscardPile)
            {
                card.gameObject.SetActive(true);
            }
        }

        foreach(LineRenderer line in generateWriting.drawn)
        {
            Destroy(line);
        }

        StateManager.currentState = StateManager.GameState.SpellSelect;
        Destroy(spellManager.wordInstance);

        this.gameObject.SetActive(false);

    }
}
