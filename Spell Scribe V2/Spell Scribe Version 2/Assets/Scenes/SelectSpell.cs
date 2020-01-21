using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectSpell : MonoBehaviour
{
    public SpawnWord spellSet;

    public void Choose()
    {
        spellSet.Spawn(GetComponentInChildren<TextMeshProUGUI>().text);
    }

}
