using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHelpCloseButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
