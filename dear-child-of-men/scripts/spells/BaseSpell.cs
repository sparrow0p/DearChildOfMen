using Godot;
using System;


[Tool]
[GlobalClass]
public partial class BaseSpell : Node3D {
    [Export] private Combat.ElementTypeEnum PrimaryElement {get; set;}
    [Export] private Node3D SpellAreaNode {get; set;}
    [Export] public SpellArea[] SpellAreaArray {get; set;} = Array.Empty<SpellArea>();


    public override void _EnterTree() {
        base._EnterTree();
        SpellAreaNode = GetChild<Node3D>(0);
    }
    

    public override void _Ready() {
        connect_areas();
    }


    private void connect_areas() {
        int id = 0;

        foreach (SpellArea spell_area in SpellAreaArray) {
            spell_area.AreaEntered += (area) => area_entered_spell(spell_area, area);
            spell_area.AreaExited += (area) => area_exited_spell(spell_area, area);
            spell_area.BodyEntered += (body) => body_entered_spell(spell_area, body);
            spell_area.BodyExited += (body) => body_exited_spell(spell_area, body);
            spell_area.Id = id;
            id++;
        }
    }


    public void area_entered_spell(Area3D spell_area, Area3D area) {
        
    }


    public void area_exited_spell(Area3D spell_area, Area3D area) {
        
    }


    public void body_entered_spell(Area3D spell_area, Node3D body) {
        
    }


    public void body_exited_spell(Area3D spell_area, Node3D body) {
        
    }
}
