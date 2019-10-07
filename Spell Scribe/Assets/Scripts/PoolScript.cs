using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{

    public int poolLength = 15;
    float timer;
    float maxScale;
    bool reducing = false;
    // Update is called once per frame
    private void Start()
    {
        maxScale = this.transform.localScale.x;
        this.transform.localScale = new Vector3(Scale, Scale);

        StartCoroutine(Enlarge());
        timer = 0.0f;
    }
    void FixedUpdate()
    {
        timer += 1 * Time.deltaTime;
        if (timer >= poolLength)
        {
            if (!reducing)
            {
                StartCoroutine(Reduce());
            }
        }
    }

    float Scale = 0.1f;

    IEnumerator Enlarge()
    {
        while(Scale < maxScale)
        {

            Scale += .25f * Time.deltaTime;
            transform.localScale = new Vector2(Scale, Scale);
            yield return null;
        }
    }

    IEnumerator Reduce()
    {
        reducing = true;
        while (Scale > 0)
        {

            Scale -= .4f * Time.deltaTime;
            transform.localScale = new Vector2(Scale, Scale);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
