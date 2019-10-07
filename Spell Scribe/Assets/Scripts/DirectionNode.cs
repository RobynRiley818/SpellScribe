using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionNode : MonoBehaviour {

    public GameObject next;

    public GameObject[] normal, flight;
    
    public DirectionNode()
    {
        
    }

    public GameObject getNext(string type)
    {
        int num;
        if (type.Equals("Walk"))
        {
            if (normal.Length == 1)
            {
                next = normal[0];
            }
            else if(normal.Length > 1)
            {
                num = Random.Range(0, normal.Length);
                next = normal[num];
            }
        }
        if (type.Equals("Fly"))
        {
            if (flight.Length == 1)
            {
                next = flight[0];
            }
            else if(flight.Length > 1)
            {
                num = Random.Range(0, flight.Length);
                next = flight[num];
            }
        }
        return next;
    }
}
