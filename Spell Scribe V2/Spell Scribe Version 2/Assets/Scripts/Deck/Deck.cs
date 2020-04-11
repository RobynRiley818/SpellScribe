using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<BaseCard> SpellDeck;
    public List<BaseCard> drawPile;
    public List<BaseCard> discardPile;

    private int deckSize;
    private int nextCard;
    public List<BaseCard> newHand;

    private int currentCard;
    private SpellManager spellManage;

    private void Start()
    {
        spellManage = FindObjectOfType<SpellManager>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void ResetDecks()
    {
        drawPile = new List<BaseCard>(SpellDeck);
        deckSize = drawPile.Count;
        nextCard = 0;
        newHand.Clear();
        discardPile.Clear();
    }

    public List<BaseCard> drawNewHand()
    {
        newHand = new List<BaseCard>(newHand);

        for (int i = 0; i < 5; i++)
        {
            if (drawPile.Count > 0)
            {
                newHand.Add(drawPile[nextCard]);
                drawPile.RemoveAt(nextCard);
                newHand[nextCard].GetComponent<BaseCard>().ResetCard();
            }

            else
            {
                ShuffleDiscard();
                newHand.Add(drawPile[nextCard]);
                drawPile.RemoveAt(nextCard);
                newHand[nextCard].GetComponent<BaseCard>().ResetCard();
            }
        }
        return newHand;
    }

    public void SetCurrentCard(int num)
    {
        currentCard = num;
    }

    public void ShuffleDiscard()
    {
        Debug.Log("Shuffling");
        for (int i = 0; i < discardPile.Count; i++)
        {
            BaseCard temp = discardPile[i];
            int randomIndex = Random.Range(i, discardPile.Count);
            discardPile[i] = discardPile[randomIndex];
            discardPile[randomIndex] = temp;
        }

        drawPile = new List<BaseCard>(discardPile);
        nextCard = 0;
        discardPile.Clear();
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < drawPile.Count; i++)
        {
            BaseCard temp = drawPile[i];
            int randomIndex = Random.Range(i, drawPile.Count);
            drawPile[i] = drawPile[randomIndex];
            drawPile[randomIndex] = temp;
        }
    }

    public void DiscardCard(BaseCard card)
    {
        for(int i = 0; i < 5; i++)
        {
            if(card.thisSpellsSecondaryEffect == newHand[i].thisSpellsSecondaryEffect)
            {
                if(card.word == newHand[i].word)
                {
                    newHand.RemoveAt(i);
                    discardPile.Add(card);
                    newHand.Add(DrawOneCard());
                    break;
                }
            }
        }
   
    }

    public BaseCard DrawOneCard()
    {
        BaseCard newCard;
        if (drawPile.Count > 0)
        {
            newCard = drawPile[0];
            drawPile.RemoveAt(0);
        }

        else
        {
            ShuffleDiscard();
            newCard = drawPile[0];
            drawPile.RemoveAt(0);

        }

        newCard.ResetCard();
        return newCard;
    }

    public List<BaseCard> ReturnHand()
    {
        return newHand;
    }
}
