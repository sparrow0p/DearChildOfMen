using Godot;
using System;


[Tool]
[GlobalClass]
public partial class BaseSpell : Node3D {
    #region Export Variables
    [Export] public Combat.ElementTypeEnum PrimaryElement {get; set;}
    [Export] public Combat.ElementTypeEnum SecondaryElement {get; set;}
    [Export] public Node3D SpellAreaNode {get; set;}
    [Export] public SpellArea[] SpellAreaArray {get; set;} = Array.Empty<SpellArea>();
    #endregion

    #region Other Variables
    public Vector2 Direction {get; set;} = new();
    #endregion



    #region Godot Functions
    public override void _EnterTree() {
        base._EnterTree();
        SpellAreaNode = GetChild<Node3D>(0);
    }
    

    public override void _Ready() {
        ConnectAreas();
    }
    #endregion


    #region Other Functions
    private void ConnectAreas() {
        int id = 0;

        foreach (SpellArea spell_area in SpellAreaArray) {
            spell_area.AreaEntered += (area) => AreaEnteredSpell(spell_area, area);
            spell_area.AreaExited += (area) => AreaExitedSpell(spell_area, area);
            spell_area.BodyEntered += (body) => BodyEnteredSpell(spell_area, body);
            spell_area.BodyExited += (body) => BodyExitedSpell(spell_area, body);
            spell_area.Id = id;
            id++;
        }
    }


    public void AreaEnteredSpell(Area3D spell_area, Area3D area) {
        
    }


    public void AreaExitedSpell(Area3D spell_area, Area3D area) {
        
    }


    public void BodyEnteredSpell(Area3D spell_area, Node3D body) {
        
    }


    public void BodyExitedSpell(Area3D spell_area, Node3D body) {
        
    }


    public void Cast(Vector3 position, Vector2 direction) {
        GlobalPosition = position;
        GlobalRotation = Vector3.Up * -direction.Angle();
        Direction = direction;
    }
    #endregion
}
