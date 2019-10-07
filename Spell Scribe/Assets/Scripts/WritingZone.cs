using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingZone : MonoBehaviour
{

    public bool glowing;
    SpriteRenderer spr;
    public Color norm, changed;

    bool colorChanged;
    public float colTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        norm = spr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.spellReady)
        {
            if (!glowing)
            {
                StartCoroutine(Glow());
            }
            colTime += .33f * Time.deltaTime;
        }
        else
        {
            spr.color = norm;
        }

    }


    IEnumerator Glow()
    {
        glowing = true;
        while (GameManager.spellReady)
        {
            if(!colorChanged)
            {
                spr.color = Color.Lerp(norm, changed, colTime);
                if(spr.color == changed)
                {
                    colTime = 0;
                    colorChanged = true;
                }
            }
            else
            {
                spr.color = Color.Lerp(changed, norm, colTime);
                if (spr.color == norm)
                {
                    colTime = 0;
                    colorChanged = false;
                }
            }

            yield return null;
        }

        glowing = false;
    }
}
