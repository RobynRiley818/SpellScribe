using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyScreen : MonoBehaviour
{
    public Button easy, normal, hard;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        easy = FindObjectsOfType<Button>()[2];
        normal = FindObjectsOfType<Button>()[3];
        hard = FindObjectsOfType<Button>()[0];

        manager = GameObject.Find("Game Manager(Clone)");

        easy.onClick.AddListener(delegate { manager.GetComponent<GameManager>().setDifficulty(GameManager.Difficulty.Easy); });
        normal.onClick.AddListener(delegate { manager.GetComponent<GameManager>().setDifficulty(GameManager.Difficulty.Normal); });
        hard.onClick.AddListener(delegate { manager.GetComponent<GameManager>().setDifficulty(GameManager.Difficulty.Hard); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
