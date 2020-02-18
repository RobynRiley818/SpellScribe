using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseCard : MonoBehaviour
{
    public string word;
    private SpellManager spellManage;
    private GenerateSpellCards cardManager;

    public TextMeshProUGUI text;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        spellManage = FindObjectOfType<SpellManager>();
        cardManager = FindObjectOfType<GenerateSpellCards>();
        text.text = word;
    }

    private void OnMouseDown()
    {
        spellManage.Spawn(word);
        spellManage.spellDamage = damage;
    }
}
