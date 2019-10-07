using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawingManager : MonoBehaviour 
{
	[SerializeField] GameObject startingCircle;
	[SerializeField] GameObject correctLineRenderer;
	[SerializeField] GameObject incorrectLineRenderer;
	[SerializeField] int numLinesRembered;
	GameObject correctLineObject;
	GameObject lineObject;
	LineRenderer correctLine;
	GameObject incorrectLineObject;
	LineRenderer incorrectLine;
	public static LinkedList<GameObject> PreviousLines;
	int correctIndex = 0;
	int index = 0;
	bool startedDrawing = false;
	public bool drawing = false;
	bool correct = false;
    public static bool stayed;
    public bool stayTest;

    public GameObject selectMenu, drawMenu;

    public static Button hold;

    public GameObject backButton;

    GameManager manage;

	private void Start() 
	{
		PreviousLines = new LinkedList<GameObject>();
        stayed = true;
        manage = GameObject.Find("Game Manager(Clone)").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () 
	{
        stayTest = stayed;
		if (Input.GetMouseButton(0) && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < -2.5 && manage.canWrite &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x > -3.1 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 3.1 && !TutorialManager.blocking)

        {
            if (!correct && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < -2.5 && 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x > -3.1 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 3.1)
            {
                stayed = false;
            }
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Input.GetMouseButtonDown(0))
			{
				drawing = true;
				startedDrawing = true;
				correctLineObject = Instantiate(correctLineRenderer, Vector3.zero, Quaternion.identity, null);
				incorrectLineObject = Instantiate(incorrectLineRenderer, Vector3.zero, Quaternion.identity, null);
				incorrectLine = incorrectLineObject.GetComponent<LineRenderer>();
                correctLine = correctLineObject.GetComponent<LineRenderer>();
			}

			if (correct)
			{
				if (startedDrawing)
				{
					GameObject temp = Instantiate(startingCircle, pos, Quaternion.identity, correctLineObject.transform);
					temp.GetComponent<SpriteRenderer>().color = Color.green;
					temp.GetComponent<SpriteRenderer>().sortingOrder++;
				}
				/*if (correctIndex == 0)
				{
					lineObject = Instantiate(correctLineRenderer, Vector3.zero, Quaternion.identity, correctLineObject.transform);
					correctLine = lineObject.GetComponent<LineRenderer>();
				}*/
				correctLine.positionCount = correctIndex + 1;
				correctLine.SetPosition(correctIndex, pos);
				correctIndex++;
			}
			if (startedDrawing)
			{
				GameObject temp = Instantiate(startingCircle, pos, Quaternion.identity, incorrectLineObject.transform);
				temp.GetComponent<SpriteRenderer>().color = Color.red;
				startedDrawing = false;
			}
			incorrectLine.positionCount = index + 1;
			incorrectLine.SetPosition(index, pos);
			index++;


		}
		else
		{
			if (drawing)
			{
				if (PreviousLines.Count == numLinesRembered*2)
				{
					Destroy(PreviousLines.First.Value);
					PreviousLines.RemoveFirst();
					Destroy(PreviousLines.First.Value);
					PreviousLines.RemoveFirst();
				}
				drawing = false;
				PreviousLines.AddLast(correctLineObject);
				PreviousLines.AddLast(incorrectLineObject);
				index = 0;
                correctIndex = 0;

                correctLineObject = incorrectLineObject = null;
			}
		}

        /*if (Input.GetMouseButtonUp(0))
        {

            correctLineObject = incorrectLineObject = null;
            correctLine = incorrectLine = null;
        }*/

        if (manage.markers)
        {
            if (GameManager.word == null && drawMenu.activeSelf)
            {
                drawMenu.SetActive(false);
                selectMenu.SetActive(true);
            }
            if (GameManager.word != null && selectMenu.activeSelf)
            {
                GameManager.edgeColliders.Clear();
                GameManager.word = null;
                Destroy(GameManager.ins);
                GameManager.outline = null;

                GameManager.spellChosen = false;

                if (PreviousLines.Count > 0)
                {
                    foreach (GameObject i in PreviousLines)
                    {
                        Destroy(i);
                    }
                    for (int i = 0; i <= PreviousLines.Count; i++)
                    {
                        PreviousLines.RemoveFirst();
                        PreviousLines.RemoveFirst();
                    }

                }

            }

            if (selectMenu.activeSelf)
            {

                GameManager.spellReady = false;
                GameManager.spellChosen = false;
            }

            if (!manage.canWrite)
            {
                backButton.SetActive(false);
            }
            else
            {
                backButton.SetActive(true);
            }
        }
	}

    public static void Reset()
    {
        stayed = true;
    }

    public void setCorrect(bool value)
	{
		correct = value;
		correctIndex = 0;
	}

    public void Clear()
    {
        if (!GameManager.spellReady)
        {
            Reset();

            if (PreviousLines.Count > 0)
            {
                


                foreach (GameObject i in PreviousLines)
                {
                    Destroy(i);
                }
                for (int i = 0; i <= PreviousLines.Count; i++)
                {
                    PreviousLines.RemoveFirst();
                    PreviousLines.RemoveFirst();
                }
                
            }

            

            if (GameManager.word != null)
            {
                foreach (GameObject i in WordCheck.colPointer)
                {
                    if (i != null)
                    {
                        i.GetComponent<colObject>().hit = false;
                    }
                }
            }

            GameManager.backOn();

        }
    }

    public void Return()
    {
        if (manage.canWrite)
        {
            GameManager.Reset();
            manage.canWrite = false;
            Clear();
        }
    }
}
