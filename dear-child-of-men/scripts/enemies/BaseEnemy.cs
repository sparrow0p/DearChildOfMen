using Godot;
using System;


[GlobalClass]
public partial class BaseEnemy : CharacterBody3D {
    #region Export Variables
    [Export] public float Speed {get; set;} = 10.0f;
    [Export] public Combat.EnemySizeEnum Size {get; set;}
    [Export] public SpellBook SpellBook {get; set;} = new();
    [Export] public HealthComponent HealthCom {get; set;} = new();
    #endregion

    #region Other Variables
    private static PlayerBody3D Player {
		get => GlobalVar.Player;
		set => GlobalVar.Player = value;
	}
    #endregion


    #region Godot Functions
    public override void _Ready() {
        HealthCom.Dies += OnDies;
    }


    public override void _PhysicsProcess(double delta) {
        
    }
    #endregion


    #region Other Functions
    public void TakeDamage(float damage, Combat.ElementTypeEnum primary_element = Combat.ElementTypeEnum.None, Combat.ElementTypeEnum secondary_element = Combat.ElementTypeEnum.None, float knockback = default, Vector2 knockback_direction = default) {
        HealthCom?.TakeDamage(damage, primary_element, secondary_element, knockback, knockback_direction);
    }


    private void OnDies() {
        QueueFree();
    }
    #endregion
}
