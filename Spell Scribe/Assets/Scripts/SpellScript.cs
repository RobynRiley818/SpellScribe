using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpellScript : MonoBehaviour
{

    public GameObject select, writing;

    //The list of possible words the spell can be tied to
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
        int num;
        switch (GameManager.current)
        {

            case GameManager.Difficulty.Easy:
                num = Random.Range(0, manager.GetComponent<GameManager>().easyWords.Length);
                word = manager.GetComponent<GameManager>().easyWords[num];
                break;
            case GameManager.Difficulty.Normal:
                num = Random.Range(0, manager.GetComponent<GameManager>().normalWords.Length);
                word = manager.GetComponent<GameManager>().normalWords[num];
                break;
            case GameManager.Difficulty.Hard:
                num = Random.Range(0, manager.GetComponent<GameManager>().hardWords.Length);
                word = manager.GetComponent<GameManager>().hardWords[num];
                break;
        }
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
