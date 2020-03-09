using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiScripts : MonoBehaviour
{
   public void PlayButton()
    {
        //SceneManager.LoadScene("Map Scene");
        Loading.instance.Show(SceneManager.LoadSceneAsync("Map Scene"));
    }
}
