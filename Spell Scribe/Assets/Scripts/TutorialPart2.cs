using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialPart2 : MonoBehaviour
{

    public static bool finished = false;

    public Animator taps;
    public GameObject cont;

    public EdgeCollider2D edge;
    public LineRenderer line;
    Vector3 startPos;

    public GameObject correct, fix;

    GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
       
        line = FindObjectOfType<LineRenderer>();
        taps.speed = 0;
        startPos = transform.position;
        manager = GameObject.Find("Game Manager(Clone)");
        GameObject grn = GameObject.Find("green");

        finished = false;

        //manager.GetComponent<GameManager>().startSpell(grn, 1, 1, 1);
        //Destroy(grn);
        GameManager.word = grn;
        edge = FindObjectOfType<EdgeCollider2D>();

        GameManager.spellReady = false;
        GameManager.spellChosen = false;

        StartCoroutine(Trace());
    }

    int lineIndex = 0;
    private void Update()
    {
        if(!finished && Input.GetMouseButtonUp(0))
        {
            GameObject.Find("green").GetComponent<WordCheck>().Check();
        }
        if (!DrawingManager.stayed)
        {
            StartCoroutine(FixIt());
        }

        if (touching)
        {
            line.positionCount = lineIndex + 1;
            line.SetPosition(lineIndex, transform.localPosition);
            lineIndex++;
        }
        if (finished)
        {

            foreach (GameObject i in DrawingManager.PreviousLines)
            {
                Destroy(i);
            }
            for (int i = 0; i <= DrawingManager.PreviousLines.Count; i++)
            {
                DrawingManager.PreviousLines.RemoveFirst();
                DrawingManager.PreviousLines.RemoveFirst();
            }

            GameManager.spellChosen = false;
            GameManager.spellReady = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            /*if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }*/
        }
    }


    bool taughtFix = false;
    IEnumerator FixIt()
    {
        correct.SetActive(true);
        Color start = fix.GetComponent<Image>().color;
        bool red = false;
        float timer = 0;
        while (!taughtFix)
        {
            timer += Time.deltaTime;
            while (!red)
            {
                fix.GetComponent<Image>().color = Color.Lerp(fix.GetComponent<Image>().color, Color.red, timer);
                if(fix.GetComponent<Image>().color == Color.red)
                {
                    red = true;
                    timer = 0;
                }
                yield return null;
            }
            while (red)
            {
                fix.GetComponent<Image>().color = Color.Lerp(fix.GetComponent<Image>().color, Color.white, timer);
                if (fix.GetComponent<Image>().color == Color.white)
                {
                    red = false;
                    timer = 0;
                }
                yield return null;
            }
            yield return null;
        }

        correct.SetActive(false);
        fix.GetComponent<Image>().color = start;
    }

    public void Fixed()
    {
        taughtFix = true;
    }


    bool touching = false;
    IEnumerator Trace()
    {
        
        transform.position = startPos;
        taps.Play("Touch", -1, 0);
        touching = false;
        line.positionCount = 0;
        lineIndex = 0;
        int tracker = 0;
        do
        {

            while(transform.position != edge.gameObject.transform.TransformPoint(edge.points[tracker]))
            {
                transform.position = Vector3.MoveTowards(transform.position, edge.gameObject.transform.TransformPoint(edge.points[tracker]), 0.04f);
                
                yield return null;
            }
            if(tracker == 0)
            {
                taps.Play("Touch", -1, 1);
                touching = true;
            }
            tracker++;
            yield return null;
        }
        while (tracker < edge.pointCount && !Input.GetMouseButtonDown(0));

        if (!cont.activeSelf)
        {
            cont.SetActive(true);
        }

        yield return new WaitForSeconds(2);



        GameManager.spellChosen = true;
        touching = false;
        line.positionCount = 0;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
