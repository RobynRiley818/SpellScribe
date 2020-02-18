using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public BaseCard newCard;
    private TurnManager turnManage;
    private Deck playerDeck;
    private void Start()
    {
        turnManage = FindObjectOfType<TurnManager>();
        playerDeck = FindObjectOfType<Deck>();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenMap()
    {
        if(newCard != null)
        {
            playerDeck.SpellDeck.Add(newCard);
        }
        turnManage.map.MapEnable(true);
        turnManage.map.GetComponent<Map>().startingRoom.gameObject.GetComponent<DeafultRoom>().RoomDone();
    }

    public void ReloadGame()
    {
        Map map = FindObjectOfType<Map>();
        Destroy(playerDeck);
        Destroy(map.gameObject);
        SceneManager.LoadScene(0);
    }
}
