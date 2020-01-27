using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapCreation : MonoBehaviour
{
    public GameObject map;

    private void Start()
    {
        FindObjectOfType<Map>().GetComponent<Map>().MapEnable(true);
    }
}
