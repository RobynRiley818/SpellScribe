using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTracker : MonoBehaviour
{
    public int nodeIndex;

    public List<GameObject> nodes;
    void Start()
    {
        nodes = new List<GameObject>();

        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<MarkerNode>())
            {
                nodes.Add(transform.GetChild(i).gameObject);
            }
        }
        nodeIndex = 0;

        foreach(GameObject i in nodes)
        {
            i.SetActive(false);
        }

    }

    private void FixedUpdate()
    {

        if(nodeIndex >= nodes.Count)
        {
            StateManager.currentState = StateManager.GameState.WordFill;
        }
        if (!nodes[nodeIndex].gameObject.activeSelf)
        {
            foreach (GameObject i in nodes)
            {
                i.SetActive(false);
            }

            nodes[nodeIndex].SetActive(true);
        }
    }

}
