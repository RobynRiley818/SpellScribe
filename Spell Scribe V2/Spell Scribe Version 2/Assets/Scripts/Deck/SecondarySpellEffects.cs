using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SecondarySpellEffects : MonoBehaviour
{
    [HideInInspector] public string description;
    [HideInInspector] public int spellLevel;

    int baseEffect;
    int increasePerSpellLevel;

    public abstract void SpellEffect();
    public abstract string GetDescription();
    public abstract void Awake();

    protected int SpellScaling() {return baseEffect + (increasePerSpellLevel * spellLevel);}
    public void SetLevel(int level) {spellLevel = level;}
    protected void SetBaseEffect(int newBase) { baseEffect = newBase;}
    protected void SetIncreasePerLevel(int increasePerLevel) { increasePerSpellLevel = increasePerLevel;}
}
public class HealEffect : SecondarySpellEffects
{
    public override void Awake()
    {
        SetBaseEffect(2);
        SetIncreasePerLevel(1);
    }

    public override string GetDescription()
    {
        description = "Heal " + SpellScaling() + " Health";
        return description;
    }

    public override void SpellEffect()
    {
        FindObjectOfType<Player>().TakeDamage(-SpellScaling());
    }
}
