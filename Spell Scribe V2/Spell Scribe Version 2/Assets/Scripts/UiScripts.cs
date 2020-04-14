using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiScripts : MonoBehaviour
{
   public void PlayButton()
    {
        Loading.instance.Show(SceneManager.LoadSceneAsync("ShowDialouge"));
    }
}
