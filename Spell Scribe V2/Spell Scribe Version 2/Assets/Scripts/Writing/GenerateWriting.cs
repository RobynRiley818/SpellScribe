using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GenerateWriting : MonoBehaviour
{

    public float goodDistance;

    public EdgeCollider2D word;

    int index = 0;

    public GameObject good, bad;

    public LineRenderer current;

    [HideInInspector] public List<LineRenderer> drawn;

    Vector3 mousePos;

    bool close;

    SpellManager spellManager;

    private float delay = .1f;
    private float currentTime = 0;
 

    private void Start()
    {
        Input.simulateMouseWithTouches = true;
        drawn = new List<LineRenderer>();
        spellManager = FindObjectOfType<SpellManager>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (StateManager.currentState == StateManager.GameState.Writing)
        {
            if (currentTime < delay)
            {
                currentTime += Time.deltaTime;
            }

            else
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (true)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (Vector2.Distance(mousePos, word.ClosestPoint(mousePos)) <= goodDistance)
                        {
                            if (!close && current != null)
                            {
                                current = Instantiate(good, transform).GetComponent<LineRenderer>();
                                drawn.Add(current);
                                index = 0;
                            }
                            close = true;
                        }
                        else
                        {
                            if (close && current != null)
                            {
                                current = Instantiate(bad, transform).GetComponent<LineRenderer>();
                                drawn.Add(current);
                                index = 0;
                            }
                            close = false;
                        }

                        if (current == null)
                        {
                            switch (close)
                            {
                                case true:
                                    current = Instantiate(good, transform).GetComponent<LineRenderer>();
                                    drawn.Add(current);
                                    break;
                                case false:
                                    current = Instantiate(bad, transform).GetComponent<LineRenderer>();
                                    drawn.Add(current);
                                    break;
                            }
                        }

                        if (current != null)
                        {
                            current.positionCount = index + 1;
                            current.SetPosition(index, mousePos);
                            index++;
                        }

                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        current = null;
                        index = 0;
                    }
                }
                else
                {
                    if (Input.touchCount > 0)
                    {
                        Debug.Log("Calling Touch Shit");
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began)
                        {
                            if (Vector2.Distance(touch.position, word.ClosestPoint(touch.position)) <= goodDistance)
                            {
                                current = Instantiate(good, transform).GetComponent<LineRenderer>();
                                drawn.Add(current);
                                close = true;
                            }
                            else
                            {
                                current = Instantiate(good, transform).GetComponent<LineRenderer>();
                                drawn.Add(current);
                                close = false;
                            }
                        }

                        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                        {
                            if (Vector2.Distance(touch.position, word.ClosestPoint(touch.position)) <= goodDistance)
                            {
                                if (!close && current != null)
                                {
                                    current = Instantiate(good, transform).GetComponent<LineRenderer>();
                                    drawn.Add(current);
                                    index = 0;
                                }
                                close = true;
                            }
                            else
                            {
                                if (close && current != null)
                                {
                                    current = Instantiate(good, transform).GetComponent<LineRenderer>();
                                    drawn.Add(current);
                                    index = 0;
                                }
                                close = false;
                            }
                            current.positionCount = index + 1;
                            current.SetPosition(index, mousePos);
                            index++;
                        }

                        if (touch.phase == TouchPhase.Ended)
                        {
                            current = null;
                            index = 0;
                        }
                    }
                }
            }
        }
        else if (StateManager.currentState == StateManager.GameState.WordFill)
        {
            StartCoroutine(fillWord());
        }
    }

    IEnumerator fillWord()
    {
        //Word Fills will go here, if we decide to continue using that effect
        yield return null;
        for(int i = 0; i < drawn.Count; i++)
        {
            Destroy(drawn[i]);
        }
        drawn.Clear();

        StateManager.currentState = StateManager.GameState.SpellCast;
        currentTime = 0;
        current = null;
        index = 0;
        spellManager.SpellFinished();
    }
}
