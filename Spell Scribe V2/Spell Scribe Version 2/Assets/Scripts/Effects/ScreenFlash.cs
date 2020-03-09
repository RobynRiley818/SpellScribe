using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    Image image;
    Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        startColor = image.color;
        startColor.a = 0;
        image.color = startColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Flash()
    {
        Color newColor = startColor;
        newColor.a = 1;
        startColor.a = 1;
        yield return new WaitForSeconds(.1f);

        while(newColor.a > 0)
        {
            newColor.a -= .4f;
            startColor = newColor;
            image.color = startColor;
            yield return new WaitForSeconds(.1f);
        }

    }
}
