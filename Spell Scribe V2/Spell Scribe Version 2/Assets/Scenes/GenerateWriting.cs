using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWriting : MonoBehaviour
{

    public float goodDistance;

    public EdgeCollider2D word;

    int index = 0;

    public GameObject good, bad;

    public LineRenderer current;

    Vector3 mousePos;

    bool close;

    private void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (StateManager.currentState == StateManager.GameState.Writing)
        {
            if (Application.isEditor)
            {
                if (Input.GetMouseButton(0))
                {
                    if (Vector2.Distance(mousePos, word.ClosestPoint(mousePos)) <= goodDistance)
                    {
                        if (!close && current != null)
                        {
                            current = Instantiate(good, transform).GetComponent<LineRenderer>();
                            index = 0;
                        }
                        close = true;
                    }
                    else
                    {
                        if (close && current != null)
                        {
                            current = Instantiate(bad, transform).GetComponent<LineRenderer>();
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
                                break;
                            case false:
                                current = Instantiate(bad, transform).GetComponent<LineRenderer>();
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
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        if (Vector2.Distance(touch.position, word.ClosestPoint(touch.position)) <= goodDistance)
                        {
                            current = Instantiate(good, transform).GetComponent<LineRenderer>();
                            close = true;
                        }
                        else
                        {
                            current = Instantiate(bad, transform).GetComponent<LineRenderer>();
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
                                index = 0;
                            }
                            close = true;
                        }
                        else
                        {
                            if (close && current != null)
                            {
                                current = Instantiate(bad, transform).GetComponent<LineRenderer>();
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
}
