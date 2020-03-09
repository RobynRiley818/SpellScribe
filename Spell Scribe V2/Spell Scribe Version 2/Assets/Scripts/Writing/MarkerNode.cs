using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerNode : MonoBehaviour
{
    NodeTracker tracker;

    private void Start()
    {
        tracker = GetComponentInParent<NodeTracker>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && StateManager.currentState == StateManager.GameState.Writing)
        {
            Debug.Log("Over Node");
            if (tracker.nodeIndex < tracker.nodes.Count)
            {
                tracker.nodeIndex++;
            }
            gameObject.SetActive(false);
        }
    }
}
