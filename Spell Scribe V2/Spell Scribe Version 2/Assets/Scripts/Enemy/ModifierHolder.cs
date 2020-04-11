using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierHolder : MonoBehaviour
{
    [HideInInspector] public GameObject modifer;

    public Text number;

    public Text panelText;
    public GameObject panel;

    private Image image;

    [Header("Spell UI")]
    public Sprite stunUISprite;
    public Sprite burnUISprite;


    private void Awake()
    {
        image = this.GetComponent<Image>();
        image.enabled = false;
        number.enabled = false;
        panel.SetActive(false);
    }

    private void SetImage()
    {
        if (this.GetComponentInChildren<Stunned>())
        {
            image.sprite = stunUISprite;
        }

        else if(this.GetComponentInChildren<Burning>())
        {
            image.sprite = burnUISprite;
        }

        image.enabled = true;
    }

    private void OnMouseDown()
    {
        if (modifer != null)
        {
            number.enabled = true;
            image.enabled = true;
            panel.SetActive(true);
            panelText.text = modifer.GetComponent<EnemyModifier>().GetDescription();
        }
    }
    private void OnMouseUp() { panel.SetActive(false); }

    public void SetChild()
    {
        SetImage();
        number.enabled = true;
        number.text = ""+modifer.GetComponent<EnemyModifier>().effectNum;
    }
    
    public void NoChild()
    {
        image.enabled = false;

        number.enabled = false;
    }
}
