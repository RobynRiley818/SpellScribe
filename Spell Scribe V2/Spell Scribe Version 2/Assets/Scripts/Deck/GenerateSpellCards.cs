using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateSpellCards : MonoBehaviour
{
    public string[] wordOptions;
    public Vector2[] cardPositions;

    public List<GameObject> cards = new List<GameObject>();

    public GameObject spellcard;

    private List<BaseCard> currentHand;

    private Deck spellDeck;

    private void Start()
    {
        spellDeck = FindObjectOfType<Deck>();
    }

    public void Draw()
    {
        if (StateManager.currentState == StateManager.GameState.SpellSelect)
        {
            currentHand = spellDeck.drawNewHand();
            CreateHand();
        }
    }

    public void TurnOnhand()
    {
        currentHand = spellDeck.newHand;
        CreateHand();
    }

    private void CreateHand()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject ins = Instantiate(currentHand[i].gameObject, GameObject.FindGameObjectWithTag("MainCanvas").transform);

            ins.GetComponent<RectTransform>().localPosition = cardPositions[i];
            ins.SetActive(true);
        }
    }
}
