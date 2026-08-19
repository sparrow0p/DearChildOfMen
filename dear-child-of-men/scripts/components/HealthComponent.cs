using Godot;
using Godot.Collections;
using System;


[Tool]
[GlobalClass]
public partial class HealthComponent : Resource {
    #region Signals
    [Signal] public delegate void TookDamageWithArgumentEventHandler(float dmg_amount, bool zero_dmg);
    [Signal] public delegate void TookKnockbackWithArgumentEventHandler(float kb_amount, Vector2 kb_direction);
    [Signal] public delegate void WasStaggeredWithArgumentEventHandler(float stagger_amount);
    [Signal] public delegate void DiesEventHandler();
    #endregion

    #region Export Variables
    private float max_health = 10.0f;
    [Export(PropertyHint.None, "suffix:hp")] public float MaxHealth {
        get => max_health;
        set => max_health = Mathf.Max(Mathf.Round(value / 0.1f) * 0.1f, 0.0f);
    }

    [ExportGroup("Damage")]
    private float additive_inc_dmg  = 0.0f;
    [Export(PropertyHint.None, "suffix:dmg+")] public float AdditiveIncDmg {
        get => additive_inc_dmg;
        set => additive_inc_dmg = Mathf.Round(value / 0.1f) * 0.1f;
    }
    private float mult_inc_dmg  = 100.0f;
    [Export(PropertyHint.None, "suffix:%")] public float MultIncDmg {
        get => mult_inc_dmg;
        set => mult_inc_dmg = Mathf.Max(Mathf.Round(value), 0.0f);
    }
    private Dictionary<Combat.ElementTypeEnum, float> additive_inc_elemental_dmg = new();
    [Export] public Dictionary<Combat.ElementTypeEnum, float> AdditiveIncElementalDmg {
        get => additive_inc_elemental_dmg;
        set {
            additive_inc_elemental_dmg = value;
            foreach (Combat.ElementTypeEnum type in Enum.GetValues<Combat.ElementTypeEnum>()) {
                if (!additive_inc_elemental_dmg.ContainsKey(type))
                    continue;
                
                if (type == Combat.ElementTypeEnum.None) {
                    additive_inc_elemental_dmg.Remove(Combat.ElementTypeEnum.None);
                    continue;
                }
                
                additive_inc_elemental_dmg[type] = Mathf.Round(additive_inc_elemental_dmg[type] / 0.1f) * 0.1f;
            }
        }
    }
    private Dictionary<Combat.ElementTypeEnum, float> mult_inc_elemental_dmg = new();
    [Export] public Dictionary<Combat.ElementTypeEnum, float> MultIncElementalDmg {
        get => mult_inc_elemental_dmg;
        set {
            mult_inc_elemental_dmg = value;
            foreach (Combat.ElementTypeEnum type in Enum.GetValues<Combat.ElementTypeEnum>()) {
                if (!mult_inc_elemental_dmg.ContainsKey(type))
                    continue;
                
                if (type == Combat.ElementTypeEnum.None) {
                    mult_inc_elemental_dmg.Remove(Combat.ElementTypeEnum.None);
                    continue;
                }
                
                mult_inc_elemental_dmg[type] = Mathf.Max(Mathf.Round(mult_inc_elemental_dmg[type]), 0.0f);
            }
        }
    }
    
    [ExportGroup("Knockback")]
    [Export] public bool CanTakeKnockback {get; set;} = true;
    private float knockback_mult = 100.0f;
    [Export(PropertyHint.None, "suffix:%")] public float KnockbackMult {
        get => knockback_mult;
        set => knockback_mult = Mathf.Max(Mathf.Round(value), 0.1f);
    }

    [ExportGroup("Stagger")]
    [Export] public bool CanBeStaggered {get; set;} = true;
    private float stagger_mult = 100.0f;
    [Export(PropertyHint.None, "suffix:%")] public float StaggerMult {
        get => stagger_mult;
        set => stagger_mult = Mathf.Max(Mathf.Round(value), 0.1f);
    }
    #endregion
    
    #region Other Variables
    public float Health {get; set;}
    #endregion
    

    #region Other Functions
    public void TakeDamage(float damage, Combat.ElementTypeEnum primary_element = Combat.ElementTypeEnum.None, Combat.ElementTypeEnum secondary_element = Combat.ElementTypeEnum.None, 
    float knockback = default, Vector2 knockback_direction = default, float stagger_time = default) {
        float applied_dmg = damage;
        
        applied_dmg = Mathf.Max(applied_dmg + AdditiveIncDmg, 0.0f);

        if (additive_inc_elemental_dmg.ContainsKey(primary_element))
            applied_dmg = Mathf.Max(applied_dmg + AdditiveIncElementalDmg[primary_element], 0.0f);
        
        if (additive_inc_elemental_dmg.ContainsKey(secondary_element))
            applied_dmg = Mathf.Max(applied_dmg + 0.5f * AdditiveIncElementalDmg[secondary_element], 0.0f);

        applied_dmg *= MultIncDmg;

        if (mult_inc_elemental_dmg.ContainsKey(primary_element))
            applied_dmg *= mult_inc_elemental_dmg[primary_element];

        if (mult_inc_elemental_dmg.ContainsKey(primary_element))
            applied_dmg *= mult_inc_elemental_dmg[primary_element];

        applied_dmg = Mathf.Round(applied_dmg / 0.1f) * 0.1f;

        if (CanTakeKnockback && knockback != default && knockback_direction != default && applied_dmg != 0)
            EmitSignalTookKnockbackWithArgument(knockback * KnockbackMult * applied_dmg / damage, knockback_direction);

        if (CanBeStaggered && stagger_time != default && applied_dmg != 0)
            EmitSignalWasStaggeredWithArgument(stagger_time * StaggerMult * applied_dmg / damage);

        applied_dmg = Mathf.Min(applied_dmg, Health);

        Health -= applied_dmg;
        EmitSignalTookDamageWithArgument(applied_dmg, applied_dmg == 0);

        if (Health == 0)
            EmitSignalDies();
    }
    #endregion
}
