using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialougeManager : MonoBehaviour
{
    public GameObject[] dialouges;

    private Dialouge currentDialouge;
    private int currentDialougeInt;
    private bool onDialouge;

    void Start()
    {
        TurnOffDialouges();
        currentDialougeInt = 0;
        onDialouge = false;
        NextLine();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        if (onDialouge)
        {
            currentDialouge.NextLine();
        }

        else
        {
            if(currentDialougeInt < dialouges.Length)
            {
                TurnOffDialouges();
                dialouges[currentDialougeInt].SetActive(true);
                currentDialouge = dialouges[currentDialougeInt].gameObject.GetComponent<Dialouge>();
                currentDialougeInt += 1;
                onDialouge = true;
            }

            else
            {
                LoadNextScene();
            }
        }
    }

    public void DialougeDone()
    {
        onDialouge = false;
        NextLine();
    }

    private void TurnOffDialouges()
    {
        foreach(GameObject g in dialouges)
        {
            g.SetActive(false);
        }
    }

    private void LoadNextScene()
    {
        Loading.instance.Show(SceneManager.LoadSceneAsync("Map Scene"));
    }
}
