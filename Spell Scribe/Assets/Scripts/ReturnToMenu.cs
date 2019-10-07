using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    TutorialManager man;

    private void Start()
    {
        man = Camera.main.GetComponent<TutorialManager>();
    }
    public void Return()
    {
        SceneManager.LoadScene(0);
    }
    public void Skip()
    {
        man.current = TutorialManager.teachingPhase.Done;
    }
}
