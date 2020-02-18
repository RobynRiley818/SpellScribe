using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeafultRoom : MonoBehaviour
{
    bool isSelectable = false;
    public bool startingRoom = false;
    public GameObject[] nextRooms = new GameObject[0];
    private GameObject map;
    private LineRenderer line;
    SpriteRenderer sr;
    public string[] roomOptions = new string[1];
    string thisRoom;
    public bool finalRoom = false;
    // Start is called before the first frame update

    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();

  
        Color temp = sr.color;
        temp.a = .5f;
        sr.color = temp;
        AssignRandomRoom();

        map = FindObjectOfType<Map>().gameObject;
        DrawnLine();
        if (startingRoom)
        {
            this.isSelectable = false;
            Invoke("RoomDone", .1f);
            map.GetComponent<Map>().startingRoom = this.gameObject;
        }
  
    }
    public void RoomDone()
    {
        if (finalRoom)
        {
            Invoke("Restart", 1);
     
        }
        for (int i = 0; i < nextRooms.Length; i++)
        {
            Debug.Log("Test");
            nextRooms[i].GetComponent<DeafultRoom>().Selectable(true);
            SpriteRenderer temp = nextRooms[i].GetComponent<SpriteRenderer>();
            Color tempColor = temp.color;
            tempColor.a = 1;
            temp.color = tempColor;
        }
    }

    public void NextRoom()
    {
        Debug.Log("Next Room");
        GameObject[] allRooms = GameObject.FindGameObjectsWithTag("Room");
        foreach(GameObject a in allRooms)
        {
            SpriteRenderer temp = a.GetComponent<SpriteRenderer>();
            Color tempColor = temp.color;
            tempColor.a = .5f;
            temp.color = tempColor;
            a.GetComponent<DeafultRoom>().Selectable(false);
        }
    }

    public void Selectable(bool selectable)
    {
        isSelectable = selectable;
    }

    private void OnMouseDown()
    {
        if (isSelectable)
        {
           
            Debug.Log(thisRoom);
            NextRoom();
            map.GetComponent<Map>().startingRoom.GetComponent<DeafultRoom>().startingRoom = false;
            map.GetComponent<Map>().startingRoom = this.gameObject;
            map.GetComponent<Map>().MapEnable(false);
            SceneManager.LoadScene(thisRoom);
       
            
        }
    }

    private void DrawnLine()
    {
        for (int i = 0; i < nextRooms.Length; i++)
        {
            line = this.gameObject.transform.GetChild(i).gameObject.AddComponent<LineRenderer>();
            line.positionCount = 2;
            line.startWidth = .1f;
            line.startColor = Color.black;
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, nextRooms[i].transform.position);
        }
    }

    private void AssignRandomRoom()
    {
        int num = Random.Range(0, roomOptions.Length);
        thisRoom = roomOptions[num];
    }

    private void Restart()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("Main Menu");
        Destroy(this.gameObject.transform.parent.gameObject);
    }

}
