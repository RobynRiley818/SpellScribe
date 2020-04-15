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

    public enum SecondaryEffects { heal, burn, stun }
    [Range(1, 3)] public int spellLevel = 1;

    public SecondaryEffects thisSpellsSecondaryEffect;
    SecondarySpellEffects thisSecondaryEffect;

    public enum SpellVisual { fireball }
    public SpellVisual visual;

    public GameObject fireBolt;
    private GameObject spellEffect;

    private bool exampleCard = false;

    private bool pressed;
    private float timePressedTillDescription = 1;
    private float currentTimePressed = 0;

    [HideInInspector] public bool inDiscardPile = false;
    public GameObject cardHelp;
    private GameObject thisCardHelp;

    private GameObject tempCard;

    private TextMeshProUGUI helpWord;
    private TextMeshProUGUI helpWordDef;
    private TextMeshProUGUI helpEffect;
    private TextMeshProUGUI helpEffectDef;
    private TextMeshProUGUI helpDamageDef;

    // Start is called before the first frame update
    void Start()
    {
        thisCardHelp = Instantiate(cardHelp, FindObjectOfType<Canvas>().transform);

        helpWord = thisCardHelp.transform.Find("Word").GetComponent<TextMeshProUGUI>();
        helpWordDef = thisCardHelp.transform.Find("WordDef").GetComponent<TextMeshProUGUI>();

        helpEffect = thisCardHelp.transform.Find("Effect").GetComponent<TextMeshProUGUI>();
        helpEffectDef = thisCardHelp.transform.Find("EffectDef").GetComponent<TextMeshProUGUI>();
        helpDamageDef = thisCardHelp.transform.Find("DamDef").GetComponent<TextMeshProUGUI>();

        thisCardHelp.SetActive(false);
        spellManage = FindObjectOfType<SpellManager>();
        cardManager = FindObjectOfType<GenerateSpellCards>();
        SetSecondaryEffect();
        SetVisual();
        thisSecondaryEffect.spellLevel = spellLevel;
        secondaryDescription.text = thisSecondaryEffect.GetDescription();
        text.text = word;
        damageText.text = "" + damage;
        currentTimePressed = 0;
    }

    private void OnEnable()
    {
        thisCardHelp = Instantiate(cardHelp, FindObjectOfType<Canvas>().transform);

        helpWord = thisCardHelp.transform.Find("Word").GetComponent<TextMeshProUGUI>();
        helpWordDef = thisCardHelp.transform.Find("WordDef").GetComponent<TextMeshProUGUI>();

        helpEffect = thisCardHelp.transform.Find("Effect").GetComponent<TextMeshProUGUI>();
        helpEffectDef = thisCardHelp.transform.Find("EffectDef").GetComponent<TextMeshProUGUI>();
        helpDamageDef = thisCardHelp.transform.Find("DamDef").GetComponent<TextMeshProUGUI>();

        thisCardHelp.SetActive(false);
        spellManage = FindObjectOfType<SpellManager>();
        cardManager = FindObjectOfType<GenerateSpellCards>();
        SetSecondaryEffect();
        SetVisual();
        thisSecondaryEffect.spellLevel = spellLevel;
        secondaryDescription.text = thisSecondaryEffect.GetDescription();
        text.text = word;
        damageText.text = "" + damage;
        currentTimePressed = 0;
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
            if (currentTimePressed < timePressedTillDescription && !thisCardHelp.activeSelf)
            {
                StartCoroutine(MouseRelease());
                spellManage.Spawn(word);
                spellManage.spellDamage = damage;
                spellManage.spellEffect = spellEffect;
                spellManage.secondary = thisSecondaryEffect;
                spellManage.currentCard = this.gameObject;
                StopCoroutine(MouseRelease());
                pressed = false;
                currentTimePressed = 0;
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

            if (currentTimePressed >= timePressedTillDescription)
            {
                OpenDescription();
            }
        }
    }

    private void OpenDescription()
    {
        StopCoroutine(MouseRelease());
        thisCardHelp.SetActive(true);
        thisCardHelp.transform.parent.SetAsLastSibling();
        tempCard = Instantiate(this.gameObject, thisCardHelp.transform);
        tempCard.GetComponent<BaseCard>().exampleCard = true;
        tempCard.transform.position = new Vector2(0, - 1);
        tempCard.transform.localScale = new Vector3(tempCard.transform.localScale.x * 2, tempCard.transform.localScale.y * 2, tempCard.transform.localScale.z * 2);
        pressed = false;
        currentTimePressed = 0;

        SetDescriptions();
    }

    private void SetDescriptions()
    {
        helpWord.text = word;
        helpWordDef.text = FindObjectOfType<Definitions>().GetDef("Be");
        helpEffect.text = thisSecondaryEffect.GetName();
        helpDamageDef.text = "Deals: " + damage + " To The Enemy's Health";
        helpEffectDef.text = FindObjectOfType<Definitions>().GetEffect(thisSecondaryEffect.GetName(), thisSecondaryEffect.SpellScaling());
    }

    public void ResetCard()
    {
        inDiscardPile = false;
    }
}
