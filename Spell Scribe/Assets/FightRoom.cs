using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightRoom : MonoBehaviour
{
    GameObject enemy;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        if (enemy == null)
        {
            StartCoroutine(Reset());
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(0);
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        map.GetComponent<Map>().MapEnable(true);
        map.GetComponent<Map>().startingRoom.gameObject.GetComponent<DeafultRoom>().RoomDone();
        SceneManager.LoadScene("Test Map");
        
    }
}
