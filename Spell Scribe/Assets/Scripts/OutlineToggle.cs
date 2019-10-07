using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OutlineToggle : MonoBehaviour
{

    Toggle outliner;
    GameObject manager;

    private void OnEnable()
    {

        outliner = FindObjectOfType<Toggle>();
        manager = GameObject.Find("Game Manager(Clone)");

        if (GameManager.wordOutlines)
        {
            outliner.isOn = true;
        }

        outliner.onValueChanged.AddListener(delegate { manager.GetComponent<GameManager>().ChangeWordOutline(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
