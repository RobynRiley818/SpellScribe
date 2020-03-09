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
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI secondaryDescription;
    public int damage;

    public enum SecondaryEffects {heal}
    [Range(1, 3)] public int spellLevel = 1; 

    public SecondaryEffects thisSpells;
    SecondarySpellEffects thisSecondaryEffect;

    public enum SpellVisual {fireball}
    public SpellVisual visual;

    public GameObject fireBolt;
    private GameObject spellEffect;
    // Start is called before the first frame update
    void Start()
    {
        spellManage = FindObjectOfType<SpellManager>();
        cardManager = FindObjectOfType<GenerateSpellCards>();
        SetSecondaryEffect();
        SetVisual();
        thisSecondaryEffect.spellLevel = spellLevel;
        secondaryDescription.text = thisSecondaryEffect.GetDescription();
        text.text = word;
        damageText.text = "" + damage;
    }

    private void OnMouseDown()
    {
        spellManage.Spawn(word);
        spellManage.spellDamage = damage;
        spellManage.spellEffect = spellEffect;
        spellManage.secondary = thisSecondaryEffect;
        spellManage.currentCard = this.gameObject;
    }

    private void SetSecondaryEffect()
    {
        switch (thisSpells)
        {
            case SecondaryEffects.heal:
                thisSecondaryEffect = gameObject.AddComponent<HealEffect>();
                break;
        }
    }

    private void SetVisual()
    {
        switch (visual)
        {
            case SpellVisual.fireball:
                spellEffect = fireBolt;
                break;
        }
    }
}
