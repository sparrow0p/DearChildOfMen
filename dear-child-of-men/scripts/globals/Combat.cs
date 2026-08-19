using Godot;
using System;
using System.Collections.Generic;


public partial class Combat {
    public enum ElementTypeEnum {
        None,
        Water,
        Earth,
        Fire,
        Air,
        Malignant,
        Eternity,
        Entropy,
        Radiation
    }
    
    public enum EnemySizeEnum {
        Small,
        Average,
        Large,
        Massive,
        Collosal,
        Godlike
    }

    public enum SpellEnum {
        ShootingStar
    }


    public static Dictionary<SpellEnum, PackedScene> SpellScenes = new() {
        {SpellEnum.ShootingStar, GD.Load<PackedScene>("uid://bq8ta7vbb5vlh")}
    };


    // temp God Why Does This Work Like This
    public static void Ready() {
        foreach(PackedScene spell_scene in SpellScenes.Values) {
            spell_scene.Instantiate<BaseSpell>();
        }
    }
}
