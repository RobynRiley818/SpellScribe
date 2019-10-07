using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPart3 : MonoBehaviour
{
    public bool finished = false;

    public Animator taps;
    public GameObject cont;

    public EdgeCollider2D edge;
    public LineRenderer line;
    Vector3 startPos;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        edge = FindObjectOfType<EdgeCollider2D>();
        line = FindObjectOfType<LineRenderer>();
        taps.speed = 0;
        startPos = transform.position;

        line.positionCount = edge.pointCount;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, edge.gameObject.transform.TransformPoint(edge.points[i]));
        }

        StartCoroutine(StylusTravel());
    }

    int lineIndex = 0;
    private void Update()
    {
    }

    public void loadNext()
    {
        Debug.Log("Clicked");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool touching = false;
    

    IEnumerator StylusTravel()
    {
        transform.position = startPos;
        while (this.transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.04f);
            yield return null;
        }

        StartCoroutine(StylusTap());
    }

    IEnumerator StylusTap()
    {
        taps.speed = 1;

        yield return new WaitForSeconds(3.0f);

        taps.speed = 0;

        yield return null;
        finished = true;


        if (!cont.activeSelf)
        {
            cont.SetActive(true);
        }

        yield return new WaitForSeconds(2.0f);
        StartCoroutine(StylusTravel());
    }
}
