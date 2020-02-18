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

    BaseCard[] cards;

    Enemy enemy;

    private bool testFix = false;


    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        writingMan = FindObjectOfType<GenerateWriting>();
        enMan = FindObjectOfType<EnemyManager>();
    }

    GameObject wordInstance;
    public void Spawn(string word)
    {
        //foreach(GameObject k in GetComponent<GenerateSpellCards>().cards)
        //{
        //    Destroy(k.gameObject);

        //}
        //GetComponent<GenerateSpellCards>().cards.Clear();

        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].name == word)
            {
                wordInstance = Instantiate(words[i], wordPos, Quaternion.identity);
                writingMan.word = wordInstance.GetComponentInChildren<EdgeCollider2D>();
                break;
            }
        }

        cards = FindObjectsOfType<BaseCard>();

        foreach(BaseCard card in cards)
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
            testFix = true;
            StateManager.currentState = StateManager.GameState.EnemyTurn;
            //if (enMan.currentEn != null)
            //{
            //    enMan.currentEn.health -= spellDamage;

            //    //Spell VFX will go here
            //}

            enemy.TakeDamage(spellDamage);
            Destroy(wordInstance);
            wordInstance = null;

            FindObjectOfType<Enemy>().StartEnemyTurn();

            Debug.Log("Trying to Kill Cards");
            foreach (BaseCard card in cards)
            {
                Destroy(card.gameObject);
            }
        }
    }
}
