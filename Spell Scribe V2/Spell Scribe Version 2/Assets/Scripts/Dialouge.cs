using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialouge : MonoBehaviour
{
    public string[] linesOfText;
    private int currentLine = 0;
    public TextMeshProUGUI dialouge;
    private string tapToContinue = "Tap Screen To Continue";

    private float normalDialougeSpeed = .1f;
    private float spedUpDialougeSpeed = .02f;
    private float currentDialougeSpeed = 0;

    private bool typingLine = false;

    DialougeManager dialougeManager;

    private void OnEnable()
    {
        NextLine();
        currentDialougeSpeed = normalDialougeSpeed;
        dialougeManager = FindObjectOfType<DialougeManager>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) & currentLine < linesOfText.Length & !typingLine)
        {
            NextLine();
        }

        else if(typingLine & Input.GetMouseButtonDown(0))
        {
            currentDialougeSpeed = spedUpDialougeSpeed;
        }
    }

    public void NextLine()
    {
        if (!typingLine)
        {
            if (currentLine < linesOfText.Length)
            {
                StartCoroutine(TypeLine());
                currentLine += 1;
            }

            else
            {
                dialougeManager.DialougeDone();
            }
        }

        else
        {
            currentDialougeSpeed = spedUpDialougeSpeed;
        }

 
    }

    IEnumerator TypeLine()
    {
        typingLine = true;

        dialouge.text = "";
        foreach(char c in linesOfText[currentLine])
        {
            dialouge.text += c;
            yield return new WaitForSeconds(currentDialougeSpeed);
        }

        dialouge.text += "<br>";
        dialouge.text += "<br>";

        foreach (char c in tapToContinue)
        {
            dialouge.text += c;
            yield return new WaitForSeconds(currentDialougeSpeed);
        }


        currentDialougeSpeed = normalDialougeSpeed;
        typingLine = false;
    }
}
