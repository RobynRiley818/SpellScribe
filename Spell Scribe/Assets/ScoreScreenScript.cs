using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenScript : MonoBehaviour
{
    Text score, defeated, escaped, cleared;

    GameObject manager;

    Button back;

    float duration = 1.0f;

    bool defFilled = false, escFilled = false, clrFilled = false;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
        defeated = GameObject.Find("Defeated").GetComponent<Text>();
        escaped = GameObject.Find("Escaped").GetComponent<Text>();
        cleared = GameObject.Find("Cleared").GetComponent<Text>();

        back = FindObjectOfType<Button>();
        back.interactable = false;

        manager = GameObject.Find("Game Manager(Clone)");

        //score.text = manager.GetComponent<GameManager>().totalScore.ToString();
        //defeated.text = manager.GetComponent<GameManager>().enemiesDefeated.ToString();
        //escaped.text = manager.GetComponent<GameManager>().enemiesEscaped.ToString();
        //cleared.text = manager.GetComponent<GameManager>().wavesCleared.ToString();

        StartCoroutine(CountTo(manager.GetComponent<GameManager>().totalScore, score));
    }

    IEnumerator CountTo(int target, Text text)
    {
        int places = 0;
        int holder = target;
        do
        {
            places++;
        } while ((holder /= 10) >= 1);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            text.text = "";
            if (places > 1)
            {
                for (int i = 0; i < places - 1; i++)
                {
                    text.text += Random.Range(0, 10);
                }
            }
            else
            {
                text.text += Random.Range(0, 10);
            }
            yield return null;
        }
        text.text = target.ToString();
        if (!defFilled)
        {
            defFilled = true;
            StartCoroutine(CountTo(manager.GetComponent<GameManager>().enemiesDefeated, defeated));
        }
        else if (!escFilled)
        {
            escFilled = true;

            back.interactable = true;
            StartCoroutine(CountTo(manager.GetComponent<GameManager>().enemiesEscaped, escaped));
        }
        else if (!clrFilled)
        {
            clrFilled = true;
            StartCoroutine(CountTo(manager.GetComponent<GameManager>().wavesCleared, cleared));
        }
    }
}
