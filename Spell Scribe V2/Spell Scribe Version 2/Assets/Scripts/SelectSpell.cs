using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectSpell : MonoBehaviour
{
    public SpellManager spellSet;

    public int damage;

    private void Start()
    {
        spellSet = FindObjectOfType<SpellManager>();
    }

    public void Choose()
    {
        spellSet.Spawn(GetComponentInChildren<TextMeshProUGUI>().text);
        spellSet.spellDamage = damage;
    }
}
