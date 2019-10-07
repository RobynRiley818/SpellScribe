using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut3Button : MonoBehaviour
{

    GameObject sty;
    // Start is called before the first frame update
    void Start()
    {
        sty = GameObject.Find("Stylus");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        sty.GetComponent<TutorialPart3>().loadNext();
    }
}
