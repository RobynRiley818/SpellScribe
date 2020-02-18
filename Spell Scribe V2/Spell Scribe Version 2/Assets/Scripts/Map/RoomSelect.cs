using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSelect : MonoBehaviour
{

    public void loadDragon()
    {
        SceneManager.LoadScene("DragonScene");
    }

    public void loadKnight()
    {
        SceneManager.LoadScene("KnightScene");
    }

    public void loadGoblin()
    {
        SceneManager.LoadScene("GoblinScene");
    }
}
