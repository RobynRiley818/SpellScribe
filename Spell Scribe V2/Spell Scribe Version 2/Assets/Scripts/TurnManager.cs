using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private StateManager states;
    private GenerateSpellCards playerDrawCards;
    private Deck playerDeck;
    public GameObject winPanel;
    public Map map;
    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
        playerDeck = FindObjectOfType<Deck>();
        playerDrawCards = FindObjectOfType<GenerateSpellCards>();
        states = FindObjectOfType<StateManager>();

        StateManager.currentState = StateManager.GameState.SpellSelect;
       
        Invoke("StartPlayerTurn", .5f);

        map = FindObjectOfType<Map>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartPlayerTurn()
    {
        playerDeck.ResetDecks();
        playerDeck.ShuffleDeck();
        playerDrawCards.Draw();
    }

    public void PlayerWon()
    {
        winPanel.SetActive(true);
    }
}
