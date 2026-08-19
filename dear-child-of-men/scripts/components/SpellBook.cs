using Godot;
using Godot.Collections;
using System;
using System.Linq;


[GlobalClass]
public partial class SpellBook : Resource {
    // private HashSet<Combat.SpellEnum> spells = default;
    // public  HashSet<Combat.SpellEnum> Spells {
    //     get => spells;
    //     set => spells = value;
    // }
    // [Export] public Dictionary<Combat.SpellEnum, bool> AvailableSpells {
    //     get {
    //         var result = new Godot.Collections.Dictionary<Combat.SpellEnum, bool>();
    //         foreach (var spell in spells)
    //             result[spell] = true;
    //         return result;
    //     }
    //     set => spells = value.Keys.ToHashSet();
    // }
    [Export] AvailableSpell[] AvailableSpells {get; set;} = default;
}
