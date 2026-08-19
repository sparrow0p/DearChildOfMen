using Godot;
using System;


[Tool]
[GlobalClass]
public partial class AvailableSpell : Resource {
	[Export] public Combat.SpellEnum Spell {get; set;}
	[Export (PropertyHint.None, "suffix:%")] public float DamageMult {get; set;} = 100.0f;
}
