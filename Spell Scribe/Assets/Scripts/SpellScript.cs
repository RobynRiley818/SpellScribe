using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpellScript : MonoBehaviour
{

    public GameObject select, writing;

    //The list of possible words the spell can be tied to
    public GameObject[] words;
    public GameObject word;

    public GameObject manager;

    public int rank;

    public int damage;

    //1 = lightning, 2 = Poison, 3 = Ice
    public int type; 

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setWord()
    {
        int num = Random.Range(0, words.Length);
        word = words[num];
    }

    private void OnMouseDown()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
        {
            if (!TutorialManager.blocking)
            {

                setWord();
                manager.GetComponent<GameManager>().startSpell(word, rank, damage, type);
                select.SetActive(false);
                writing.SetActive(true);
            }
        }
        else
        {
            setWord();
            manager.GetComponent<GameManager>().startSpell(word, rank, damage, type);
            select.SetActive(false);
            writing.SetActive(true);

        }
    }
}
