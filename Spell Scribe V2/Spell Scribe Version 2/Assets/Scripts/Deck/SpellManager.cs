using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public List<GameObject> words = new List<GameObject>();

    public Vector2 wordPos;

    GenerateWriting writingMan;

    EnemyManager enMan;

    public int spellDamage;

    [HideInInspector] public BaseCard[] cards;

    Enemy enemy;

    private bool testFix = false;

    [HideInInspector] public SecondarySpellEffects secondary;

    [HideInInspector] public GameObject currentCard;

    private Deck deck;


    [HideInInspector] public GameObject spellEffect;
    private Vector2 spellPosition = new Vector2(0, -3);
    public GameObject cancelSpellImage;
    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        writingMan = FindObjectOfType<GenerateWriting>();
        enMan = FindObjectOfType<EnemyManager>();
        deck = FindObjectOfType<Deck>();
    }

    [HideInInspector] public GameObject wordInstance;
    public void Spawn(string word)
    {
        cancelSpellImage.SetActive(true);
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].name == word)
            {
                wordInstance = Instantiate(words[i], wordPos, Quaternion.identity);
                writingMan.word = wordInstance.GetComponentInChildren<EdgeCollider2D>();
                //writingMan.word = wordInstance.GetComponentInChildren<BoxCollider2D>();
                break;
            }
        }

        cards = FindObjectsOfType<BaseCard>();

        foreach (BaseCard card in cards)
        {
            card.gameObject.SetActive(false);
        }

        StateManager.currentState = StateManager.GameState.Writing;
        testFix = false;
    }

    public void SpellFinished()
    {
        if (StateManager.currentState == StateManager.GameState.SpellCast & !testFix)
        {
            cancelSpellImage.SetActive(false);
            testFix = true;
            GameObject temp = Instantiate(spellEffect);
            temp.transform.position = spellPosition;

            secondary.SpellEffect();

            Destroy(wordInstance);
            wordInstance = null;

            currentCard.GetComponent<BaseCard>().inDiscardPile = true;
            foreach (BaseCard card in cards)
            {
                if (!card.inDiscardPile)
                {
                    Destroy(card.gameObject);
                }
            }
        }
    }

    public void SpellDamage()
    {

        enemy.TakeDamage(spellDamage);
        FindObjectOfType<Enemy>().StartEnemyTurn();

        deck.DiscardCard(currentCard.GetComponent<BaseCard>());
    }
}


