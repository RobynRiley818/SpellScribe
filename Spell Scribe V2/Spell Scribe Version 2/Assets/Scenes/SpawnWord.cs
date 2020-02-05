using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWord : MonoBehaviour
{
    public List<GameObject> words = new List<GameObject>();

    public Vector2 wordPos;

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
                GameObject ins =Instantiate(words[i], wordPos, Quaternion.identity);
                FindObjectOfType<GenerateWriting>().word = ins.GetComponent<EdgeCollider2D>();
                break;
            }
        }

        
        StateManager.currentState = StateManager.GameState.Writing;
    }
}
