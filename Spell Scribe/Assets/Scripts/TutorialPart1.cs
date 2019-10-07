using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPart1 : MonoBehaviour
{
    public Transform target;

    public bool finished = false;
    public GameObject cont;
    public Animator taps;
    Vector3 startpos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StylusTravel());
        taps = GetComponent<Animator>();
        taps.speed = 0;
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator StylusTravel()
    {
        transform.position = startpos;
        taps.Play("Touch", -1, 0);
        while(this.transform.position != target.position)
        {
            transform.position =  Vector3.MoveTowards(transform.position, target.position, 0.04f);
            yield return null;
        }

        StartCoroutine(StylusTap());
    }


    public void loadNext()
    {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
