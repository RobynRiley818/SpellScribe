using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<BaseCard> SpellDeck;
    public List<BaseCard> _spellDeck;
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
        _spellDeck = new List<BaseCard>(SpellDeck);
        deckSize = _spellDeck.Count;
        nextCard = 0;
        newHand.Clear();
        discardPile.Clear();
    }

    public List<BaseCard> drawNewHand()
    {
        newHand = new List<BaseCard>(newHand);

        for (int i = 0; i < 5; i++)
        {
            if (_spellDeck.Count > 0)
            {
                newHand.Add(_spellDeck[nextCard]);
                _spellDeck.RemoveAt(nextCard);
            }

            else
            {
                ShuffleDiscard();
                newHand.Add(_spellDeck[nextCard]);
                _spellDeck.RemoveAt(nextCard);
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

        _spellDeck = new List<BaseCard>(discardPile);
        nextCard = 0;
        discardPile.Clear();
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < _spellDeck.Count; i++)
        {
            BaseCard temp = _spellDeck[i];
            int randomIndex = Random.Range(i, _spellDeck.Count);
            _spellDeck[i] = _spellDeck[randomIndex];
            _spellDeck[randomIndex] = temp;
        }
    }

    public void DiscardCard(BaseCard card)
    {
        for(int i = 0; i < 5; i++)
        {
            if(card.thisSpells == newHand[i].thisSpells)
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
        if (_spellDeck.Count > 0)
        {
            newCard = _spellDeck[0];
            _spellDeck.RemoveAt(0);
        }

        else
        {
            ShuffleDiscard();
            newCard = _spellDeck[0];
            _spellDeck.RemoveAt(0);

        }

        return newCard;
    }

    public List<BaseCard> ReturnHand()
    {
        return newHand;
    }
}
