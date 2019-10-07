using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingBounds : MonoBehaviour 
{
	[SerializeField] public DrawingManager drawingManager;

	public void OnMouseEnter() 
	{
		drawingManager.setCorrect(true);
	}

    

	public void OnMouseExit() 
	{

		drawingManager.setCorrect(false);
	}

    public void Start()
    {
        drawingManager = GameObject.Find("Drawing Manager").GetComponent<DrawingManager>();
    }
}
