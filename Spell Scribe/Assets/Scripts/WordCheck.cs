using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordCheck : MonoBehaviour {
    public bool hitAll;
    //public GameObject[] col;

    public List<GameObject> col = new List<GameObject>();

    public static List<GameObject> colPointer;

    public Text text;
    public string nonCursive;
    public static Button hold;

    float timer;

    SpriteRenderer spr;

    public bool ready = false;

	// Use this for initialization
	void Start () {
        
        ready = true;
    }

    private void Awake()
    {
        hitAll = false;
        col.Clear();
        spr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer == 9)
            {
                col.Add(transform.GetChild(i).gameObject);
            }
        }


        colPointer = col;

        switch (GameManager.current)
        {
            case GameManager.Difficulty.Easy:
                foreach(GameObject i in col)
                {
                    if(i != null)
                    {
                        i.GetComponent<SpriteRenderer>().enabled = true;
                        i.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, .4f);
                    }
                }
                break;
            case GameManager.Difficulty.Normal:
                break;
            case GameManager.Difficulty.Hard:
                break;
        }

    }
    RaycastHit2D Node;
    // Update is called once per frame
    void Update () {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousepos.x, mousepos.y);
  

        if (!TutorialManager.blocking)
        {
             Node = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Marker"));
        }
        if (Input.GetMouseButton(0) && !TutorialManager.blocking)
        {
            if (Node)
            {
                Node.collider.gameObject.GetComponent<colObject>().hit = true;
            }
        }


        timer += Time.deltaTime;
        

        /*if(text.text.Equals("Not quite, Try Again!"))
        {
            if(timer >= 1.5f)
            {
                text.text = "Cast";
            }
        }*/
	}

    public bool areAllHit()
    {
        hit.Clear();
        foreach(GameObject i in col)
        {
            if( i != null)
            {

                hit.Add(i.GetComponent<colObject>().hit);
                
            }
        }

        foreach (GameObject i in col)
        {
            if (i != null)
            {
                if (i.GetComponent<colObject>().hit == false)
                {
                    return false;
                }
            }
        }
        return true;
    }


    public List<bool> hit = new List<bool>();
    public List<bool> prev = new List<bool>();
    public int noneHit = 0;

    public void Check()
    {
        hitAll = areAllHit();

        if(areEqual(hit, prev))
        {
            noneHit++;
        }
        else
        {
            noneHit = 0;
        }

        if (hitAll && DrawingManager.stayed)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 1") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
            {
                noneHit = 0;
                GameManager.spellReady = true;
            }
            else
            {
                TutorialPart2.finished = true;
            }
        }
        else
        {
            switch (GameManager.current)
            {
                case GameManager.Difficulty.Easy:
                    foreach (GameObject i in col)
                    {
                        if (i != null)
                        {
                            if (i.GetComponent<colObject>().hit)
                            {
                                i.GetComponent<SpriteRenderer>().enabled = false;
                                i.GetComponent<CircleCollider2D>().enabled = false;
                            }
                        }
                    }
                    break;
                case GameManager.Difficulty.Normal:
                    if(noneHit > 2)
                    {
                        foreach (GameObject i in col)
                        {
                            if (i != null)
                            {
                                if (!i.GetComponent<colObject>().hit)
                                {
                                    i.GetComponent<SpriteRenderer>().enabled = true;

                                    i.GetComponent<CircleCollider2D>().enabled = true;
                                    i.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, .4f);
                                }
                            }
                        }
                    }
                    break;
                case GameManager.Difficulty.Hard:
                    break;
            }

            prev.Clear();
            for (int i = 0; i < hit.Count; i++)
            {
                prev.Add(hit[i]);
            }

        }
        
    }


    bool areEqual(List<bool> l1, List<bool> l2)
    {
        if(l1 == null || l2 == null)
        {
            return true;
        }
        if(l1.Count != l2.Count)
        {
            return false;
        }
        else
        {
            for(int i = 0; i < l1.Count; i++)
            {
                if (l1[i] != l2[i]) return false;
            }
        }
        return true;
    }

    public void Reset()
    {
        switch (GameManager.current)
        {
            case GameManager.Difficulty.Easy:
                foreach (GameObject i in col)
                {
                    if (i != null)
                    {
                        i.GetComponent<SpriteRenderer>().enabled = true;

                        i.GetComponent<CircleCollider2D>().enabled = true;
                    }
                }
                break;
            case GameManager.Difficulty.Normal:
                if (noneHit > 2)
                {
                    foreach (GameObject i in col)
                    {
                        if (i != null)
                        {
                            if (i.GetComponent<SpriteRenderer>().enabled)
                            {
                                i.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            else
                            {
                                i.GetComponent<SpriteRenderer>().enabled = false;
                            }

                            i.GetComponent<CircleCollider2D>().enabled = true;
                        }
                    }
                }
                break;
            case GameManager.Difficulty.Hard:
                foreach (GameObject i in col)
                {
                    if (i != null)
                    {
                        i.GetComponent<CircleCollider2D>().enabled = true;
                    }
                }
                break;
        }
    }

}
