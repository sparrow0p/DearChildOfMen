using Godot;
using System;


[GlobalClass]
public partial class SpellBook : Node {
    [Export] public PackedScene[] AllSpells {get; set;} = Array.Empty<PackedScene>();
}
