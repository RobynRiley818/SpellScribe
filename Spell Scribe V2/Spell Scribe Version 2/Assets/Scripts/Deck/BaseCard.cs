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

    public enum SecondaryEffects {heal, burn, stun}
    [Range(1, 3)] public int spellLevel = 1; 

    public SecondaryEffects thisSpellsSecondaryEffect;
    SecondarySpellEffects thisSecondaryEffect;

    public enum SpellVisual {fireball}
    public SpellVisual visual;

    public GameObject fireBolt;
    private GameObject spellEffect;

    private bool exampleCard = false;

    private bool pressed;
    private float timePressedTillDescription = 5;
    private float currentTimePressed = 0;

    [HideInInspector] public bool inDiscardPile = false;
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
        if (!exampleCard)
        {
            pressed = true;
            StartCoroutine(MouseRelease());
        }
    }

    private void OnMouseUp()
    {
        if (!exampleCard)
        {
            if(currentTimePressed < timePressedTillDescription)
            {
                StartCoroutine(MouseRelease());
                spellManage.Spawn(word);
                spellManage.spellDamage = damage;
                spellManage.spellEffect = spellEffect;
                spellManage.secondary = thisSecondaryEffect;
                spellManage.currentCard = this.gameObject;
                StopCoroutine(MouseRelease());
                pressed = false;
            }
        }
    }

    private void SetSecondaryEffect()
    {
        switch (thisSpellsSecondaryEffect)
        {
            case SecondaryEffects.heal:
                thisSecondaryEffect = gameObject.AddComponent<HealEffect>();
                break;
            case SecondaryEffects.burn:
                thisSecondaryEffect = gameObject.AddComponent<BurnEffct>();
                break;
            case SecondaryEffects.stun:
                thisSecondaryEffect = gameObject.AddComponent<StunEffect>();
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

    public void ExampleCard()
    {
        exampleCard = true;
    }

    IEnumerator MouseRelease()
    {
        while (pressed)
        {
            currentTimePressed += Time.deltaTime;
            yield return new WaitForSeconds(0);

            if(currentTimePressed >= timePressedTillDescription)
            {
                OpenDescription();
            }
        }
    }

    private void OpenDescription()
    {
        StopCoroutine(MouseRelease());
        pressed = false;
    }

    public void ResetCard()
    {
        inDiscardPile = false;
    }
}
