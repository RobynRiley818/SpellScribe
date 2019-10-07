using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public enum teachingPhase { Choose, Write, Cast, Fill, Running, Null, Done};


    public Transform target;
    public teachingPhase current;

    GameManager manage;

    DrawingManager dManage;


    GameObject stylus;
    Animator taps;

    public GameObject EnemyAppeared, SelectIt, WriteIt, MagicLine, KillEm, Aftermath;

    Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        manage = GameObject.Find("Game Manager(Clone)").GetComponent<GameManager>();
        dManage = GameObject.Find("Drawing Manager").GetComponent<DrawingManager>();

        stylus = GameObject.Find("Stylus");
        taps = stylus.GetComponent<Animator>();
        taps.speed = 0;

        stylus.GetComponent<SpriteRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (current == teachingPhase.Done)
        {
            blocking = false;
            GameManager.spellChosen = false;
            GameManager.spellReady = false;
            GameManager.word = null;
            GameManager.ins = null;
            GameManager.Reset();
            manage.canWrite = false;
            manage.markers = false;
            //foreach (GameObject i in DrawingManager.PreviousLines)
            //{
            //    Destroy(i);
            //}
            //for (int i = 0; i <= DrawingManager.PreviousLines.Count; i++)
            //{
            //    DrawingManager.PreviousLines.RemoveFirst();
            //    DrawingManager.PreviousLines.RemoveFirst();
            //}
            SceneManager.LoadScene("Level 1");
        }
        else if (manage.markers && !looped)
        {
            if (!GameManager.spellChosen && current != teachingPhase.Running)
            {

                startpos = stylus.transform.position;
                current = teachingPhase.Choose;
            }
            if (GameManager.spellChosen && !GameManager.spellReady && current != teachingPhase.Null)
            {
                StopAllCoroutines();
                current = teachingPhase.Write;
            }
            if (GameManager.spellChosen && GameManager.spellReady && !manage.filled && current != teachingPhase.Running)
            {
                current = teachingPhase.Fill;
            }
            /*if(manage.filled && !MagicLine.activeSelf && current != teachingPhase.Null)
            {
                startpos = new Vector3(0, 0, 0);
                current = teachingPhase.Cast;
            }*/

        }

        if (dManage.drawing)
        {
            written = true;
        }

        if(current == teachingPhase.Choose)
        {

            StartCoroutine(spawn());
        }
        if (current == teachingPhase.Write)
        {
            StartCoroutine(teachWriting());
        }
        if(current == teachingPhase.Fill)
        {
            StartCoroutine(teachLineFill());
        }
        if(current == teachingPhase.Cast)
        {
            StartCoroutine(teachCasting());
        }


    }

    public static bool blocking;

    GameObject ins;
    IEnumerator spawn()
    {
        blocking = true;
        current = teachingPhase.Running;
        ins = Instantiate(en);

        yield return new WaitForSeconds(.1f);
        ins.GetComponentInChildren<EnemiesInterface>().pace = ins.GetComponentInChildren<EnemiesInterface>().pace*2;
        yield return new WaitForSeconds(2.0f);
        ins.GetComponentInChildren<EnemiesInterface>().pace = 0;

        EnemyAppeared.SetActive(true);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        EnemyAppeared.SetActive(false);

        blocking = false;
        StartCoroutine(teachSelect());
    }


    IEnumerator teachSelect()
    {

        SelectIt.SetActive(true);

        stylus.GetComponent<SpriteRenderer>().enabled = true;

        stylus.transform.position = startpos;

        //target = GameObject.Find("LIghtning 2").transform;
        taps.Play("Touch", -1, 0);
        while (stylus.transform.position != target.position)
        {
            stylus.transform.position = Vector3.MoveTowards(stylus.transform.position, target.position, 0.04f);
            yield return null;
        }

        StartCoroutine(StylusTap());

        yield return null;
    }


    bool looped = false;
    EdgeCollider2D edge;
    bool written = false;
    IEnumerator teachWriting()
    {
        SelectIt.SetActive(false);

        WriteIt.SetActive(true);
        taps.speed = 0;
        current = teachingPhase.Null;

        

        

        edge = GameManager.word.GetComponentInChildren<EdgeCollider2D>();
        stylus.transform.position = edge.gameObject.transform.TransformPoint(edge.points[0]);
        taps.Play("Touch", -1, 0);
        int tracker = 0;
        do
        {

            while (stylus.transform.position != edge.gameObject.transform.TransformPoint(edge.points[tracker]))
            {
                stylus.transform.position = Vector3.MoveTowards(stylus.transform.position, edge.gameObject.transform.TransformPoint(edge.points[tracker]), 0.04f);

                yield return null;
            }
            if (tracker == 0)
            {
                taps.Play("Touch", -1, 1);
            }
            tracker++;
            yield return null;
        }
        while (tracker < edge.pointCount && !written);


        if (!written)
        {

            yield return new WaitForSeconds(1.0f);
            StartCoroutine(teachWriting());
        }
        if (written)
        {
            yield return new WaitForSeconds(.25f);

            WriteIt.SetActive(false);
            stylus.GetComponent<SpriteRenderer>().enabled = false;
        }


        yield return null;
        
    }

    IEnumerator teachLineFill()
    {
        

        MagicLine.SetActive(true);
        current = teachingPhase.Running;
        blocking = true;

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        blocking = false;
        MagicLine.SetActive(false);
        StartCoroutine(teachCasting());
    }


    public GameObject en;
    IEnumerator teachCasting()
    {
        KillEm.SetActive(true);
        current = teachingPhase.Null;
        looped = true;

        taps.speed = 0;

        stylus.GetComponent<SpriteRenderer>().enabled = true;

        stylus.transform.position = startpos;

        //target = GameObject.Find("LIghtning 2").transform;
        taps.Play("Touch", -1, 0);
        while (stylus.transform.position != ins.transform.GetChild(1).position && ins != null)
        {
            if(ins == null)
            {
                break;
            }
            stylus.transform.position = Vector3.MoveTowards(stylus.transform.position, ins.transform.GetChild(1).position, 0.04f);
            yield return null;
        }

        taps.speed = 1;
        

        yield return new WaitUntil(() => ins == null);

        stylus.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(teachPost());
        
    }

    IEnumerator teachPost()
    {
        KillEm.SetActive(false);
        Aftermath.SetActive(true);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        current = teachingPhase.Done;

        yield return null;
    }

    IEnumerator StylusTap()
    {
        taps.speed = 1;

        yield return new WaitForSeconds(3.0f);

        taps.speed = 0;

        yield return null;

        yield return new WaitForSeconds(2.0f);

        StartCoroutine(teachSelect());
    }
}
