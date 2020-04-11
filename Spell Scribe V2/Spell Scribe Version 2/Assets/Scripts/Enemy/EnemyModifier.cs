using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyModifier : MonoBehaviour
{
    protected Enemy thisEnemy;
    public abstract void StartOfTurnEffect();
    public abstract void InitialEffect();

    [HideInInspector] public ModifierHolder modHolder;
    ModifierVisualBehavior modVisual;

    [HideInInspector] public int effectNum;

    protected string description;

    public void Start()
    {
        modVisual = FindObjectOfType<ModifierVisualBehavior>();

        thisEnemy = FindObjectOfType<Enemy>();

        modHolder = this.gameObject.transform.parent.gameObject.transform.GetComponent<ModifierHolder>();
    }

    private void OnDestroy()
    {
        modVisual.ReOrderModd();
    }

    public abstract string GetDescription();
}



