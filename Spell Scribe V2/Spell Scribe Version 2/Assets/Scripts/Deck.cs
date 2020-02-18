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
    public BaseCard[] newHand;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ResetDecks()
    {
        _spellDeck = new List<BaseCard>(SpellDeck);
        Debug.Log(_spellDeck.Count);
        deckSize = _spellDeck.Count;
        nextCard = 0;
    }

    public BaseCard[] drawNewHand()
    {
        newHand = new BaseCard[5];

        for (int i = 0; i < 5; i++)
        {
            if (_spellDeck.Count > 0)
            {
                newHand[i] = _spellDeck[nextCard];
                discardPile.Add(_spellDeck[nextCard]);
                _spellDeck.RemoveAt(nextCard);
            }

            else
            {
                ShuffleDiscard();
                newHand[i] = _spellDeck[nextCard];
                _spellDeck.RemoveAt(nextCard);
            }

        }

        return newHand;
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
}
