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

    public abstract string GetName();

    public int SpellScaling() {return baseEffect + (increasePerSpellLevel * spellLevel);}
    public void SetLevel(int level) {spellLevel = level;}
    protected void SetBaseEffect(int newBase) { baseEffect = newBase;}
    protected void SetIncreasePerLevel(int increasePerLevel) { increasePerSpellLevel = increasePerLevel;}
}

#region Support Spell Effects
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

    public override string GetName()
    {
        return "Heal";
    }

    public override void SpellEffect()
    {
        FindObjectOfType<Player>().TakeDamage(-SpellScaling());
    }

    #endregion
}

#region Offensive Spell Effects
public class StunEffect : SecondarySpellEffects
{
    ModifierVisualBehavior modVisual;
    public override void Awake()
    {
        modVisual = FindObjectOfType<ModifierVisualBehavior>();
        SetBaseEffect(3);
        SetIncreasePerLevel(0);
    }

    public override string GetDescription()
    {
        description = "Stun " + SpellScaling();
        return description;
    }

    public override string GetName()
    {
        return "Stun";
    }

    public override void SpellEffect()
    {
        modVisual.AddModd(ModifierVisualBehavior.EnemyModTypes.stun, SpellScaling());
    }
}

public class BurnEffct : SecondarySpellEffects
{
    ModifierVisualBehavior modVisual;
    public override void Awake()
    {
        modVisual = FindObjectOfType<ModifierVisualBehavior>();
        SetBaseEffect(2);
        SetIncreasePerLevel(1);
    }

    public override string GetDescription()
    {
        description = "Burn " + SpellScaling(); 
        return description;
    }

    public override string GetName()
    {
        return "Burn";
    }

    public override void SpellEffect()
    {
        modVisual.AddModd(ModifierVisualBehavior.EnemyModTypes.burn, SpellScaling());
    }

    #endregion
}
