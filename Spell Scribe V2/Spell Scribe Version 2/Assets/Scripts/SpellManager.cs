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

    private void Start()
    {
        writingMan = FindObjectOfType<GenerateWriting>();
        enMan = FindObjectOfType<EnemyManager>();
    }


    private void Update()
    {
        if(StateManager.currentState == StateManager.GameState.SpellCast)
        {
            if(enMan.currentEn != null)
            {
                enMan.currentEn.health -= spellDamage;

                //Spell VFX will go here
            }
            Destroy(wordInstance);
            wordInstance = null;
            StateManager.currentState = StateManager.GameState.SpellSelect;
            
        }
    }

    GameObject wordInstance;
    public void Spawn(string word)
    {
        foreach(GameObject k in GetComponent<GenerateSpellCards>().cards)
        {
            Destroy(k.gameObject);
            
        }
        GetComponent<GenerateSpellCards>().cards.Clear();

        for (int i = 0; i < words.Count; i++)
        {
            if(words[i].name == word)
            {
                 wordInstance = Instantiate(words[i], wordPos, Quaternion.identity);
                writingMan.word = wordInstance.GetComponentInChildren<EdgeCollider2D>();
                break;
            }
        }

        
        StateManager.currentState = StateManager.GameState.Writing;
    }
}
