using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    
    public GameObject startingRoom;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //MapEnable(false);
    }
    public void MapEnable(bool enable)
    {
        this.GetComponent<SpriteRenderer>().enabled = enable;
        foreach(Transform child in this.transform)
        {
            child.gameObject.SetActive(enable);
        }
    }
}
