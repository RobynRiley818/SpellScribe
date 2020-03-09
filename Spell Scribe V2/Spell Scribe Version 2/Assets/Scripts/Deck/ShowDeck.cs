using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeck : MonoBehaviour
{
    public GameObject cardPanel;
    private Canvas canvas;
    private RectTransform rect;

    private float distanceBetweenCardsX;
    private float distanceBetweenCardsY;

    BaseCard tempCard;
    Vector3 newCardPosition;
    public enum DifferentDecks{currentDeck, discard, fullDeck}
    public DifferentDecks whichDeck;

    private Deck deck;
    public bool showDeck = true;
    // Start is called before the first frame update
    void Start()
    {
        deck = FindObjectOfType<Deck>();
        FindDistanceBetweenCards();
    }

    private void FindDistanceBetweenCards()
    {
        canvas = FindObjectOfType<Canvas>();
        rect = canvas.GetComponent<RectTransform>();
        distanceBetweenCardsX = rect.rect.width / 5;
        distanceBetweenCardsY = rect.rect.height / 5;
    }

    public void ShowCards(List<BaseCard> cards)
    {
        cardPanel.SetActive(true);
        cardPanel.transform.SetAsLastSibling();
        int numberOfCards = cards.Count;
        int currentCard = 0;
        if(numberOfCards <= 5)
        {
            for(int i = 0; i < numberOfCards; i++)
            {
                Debug.Log("First Spawning");
                tempCard = Instantiate(cards[currentCard], cardPanel.transform);
                newCardPosition.y = (0 + (distanceBetweenCardsY * 2));
                newCardPosition.x = ((-distanceBetweenCardsX * 2) + (distanceBetweenCardsX * i));
                currentCard++;
                Debug.Log(newCardPosition);
                tempCard.GetComponent<RectTransform>().anchoredPosition = newCardPosition;
                Destroy(tempCard.GetComponent<BaseCard>());
            }
        }

        else
        {
            for(int row = 0; row <= (numberOfCards/5); row++)
            {
                for (int coll = 0; coll < 4; coll++)
                {
                    Debug.Log("Second Spawning");
                    tempCard = Instantiate(cards[currentCard], cardPanel.transform);
                    newCardPosition.y = ((distanceBetweenCardsY * 2) - (distanceBetweenCardsY * row));
                    newCardPosition.x = ((-distanceBetweenCardsX * 2) + (distanceBetweenCardsX * coll));
                    currentCard++;
                    tempCard.GetComponent<RectTransform>().anchoredPosition = newCardPosition;
                }
            }
        }
    }

    public void UnShow()
    {
        foreach (Transform child in cardPanel.transform)
        {
            if (child.gameObject.GetComponent<BaseCard>())
            {
                Destroy(child.gameObject);
            }
        }

        cardPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (showDeck)
        {
            switch (whichDeck)
            {
                case DifferentDecks.currentDeck:
                    ShowCards(deck._spellDeck);
                    break;
                case DifferentDecks.discard:
                    ShowCards(deck.discardPile);
                    break;
                case DifferentDecks.fullDeck:
                    ShowCards(deck.SpellDeck);
                    break;
            }
        }

        else
        {
            UnShow();
        }

    }
}
