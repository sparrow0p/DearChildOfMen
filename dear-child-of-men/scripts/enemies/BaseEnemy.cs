using Godot;
using System;


[GlobalClass]
public partial class BaseEnemy : CharacterBody3D {
    #region Export Variables
    [Export] public float Speed {get; set;} = 10.0f;
    [Export] public Combat.EnemySizeEnum Size {get; set;}
    [ExportCategory("Components")]
    [Export] public SpellBook SpellBook {get; set;} = new();
    [Export] public HealthComponent HealthCom {get; set;} = new();
    [ExportCategory("Node Links")]
    [Export] public NavigationAgent3D NavigationAgent {get; set;}
    [Export] public RayCast3D IdealPositionRayCast {get; set;}
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


    public override void _Input(InputEvent @event) {
        if (@event.IsActionPressed("2")) {
            Callable.From(NavigationActorSetup).CallDeferred();
        }
    }


    public override void _PhysicsProcess(double delta) {
        NavigationActorMove();

        MoveAndSlide();
    }
    #endregion


    #region Other Functions
    public void TakeDamage(float damage, Combat.ElementTypeEnum primary_element = Combat.ElementTypeEnum.None, Combat.ElementTypeEnum secondary_element = Combat.ElementTypeEnum.None, float knockback = default, Vector2 knockback_direction = default) {
        HealthCom?.TakeDamage(damage, primary_element, secondary_element, knockback, knockback_direction);
    }


    private void OnDies() {
        QueueFree();
    }


    private async void NavigationActorSetup() {
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        if (FindIdealRangedPosition(Player.GlobalPosition, 10.0f) is Vector3 target_position)
            NavigationAgent.TargetPosition = target_position;
    }


    private void NavigationActorMove() {
        if (NavigationAgent.IsNavigationFinished()) {
            Velocity = Vector3.Zero;
            return;
        }

        Vector3 currentAgentPosition = GlobalTransform.Origin;
        Vector3 nextPathPosition = NavigationAgent.GetNextPathPosition();

        Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * 10.0f;
    }


    private Vector3? FindIdealRangedPosition(Vector3 target_position, float ideal_distance, float min_distance = 0.0f, int ray_casts_number = 16, float parent_target_distance_ratio = 0.5f) {
        Vector3 IdealPosition = Vector3.Zero;
        float min_measure = float.PositiveInfinity;

        IdealPositionRayCast.Enabled = true;
        IdealPositionRayCast.GlobalPosition = target_position;
        IdealPositionRayCast.TargetPosition = Vector3.Right * ideal_distance;
        parent_target_distance_ratio = Mathf.Clamp(parent_target_distance_ratio, 0.0f, 1.0f);

        for (int i = 0; i < ray_casts_number; i++) {
            IdealPositionRayCast.ForceRaycastUpdate();

            Vector3 collision_point = IdealPositionRayCast.IsColliding() ? IdealPositionRayCast.GetCollisionPoint() : target_position + IdealPositionRayCast.TargetPosition;
            IdealPositionRayCast.TargetPosition = IdealPositionRayCast.TargetPosition.Rotated(Vector3.Up, 2 * Mathf.Pi / 16);

            float parent_distance_to_collision = GlobalPosition.DistanceTo(collision_point);
            float target_distance_to_collision = target_position.DistanceTo(collision_point);
            

            if (target_distance_to_collision < min_distance)
                continue;

            float ideal_distance_to_collision = Mathf.Abs(ideal_distance - target_distance_to_collision);
            float distance_sum = parent_distance_to_collision * parent_target_distance_ratio + ideal_distance_to_collision * (1.0f - parent_target_distance_ratio);

            if (distance_sum < min_measure) {
                IdealPosition = collision_point;
                min_measure = distance_sum;
            }
        }

        IdealPositionRayCast.Enabled = false;

        if (IdealPosition == Vector3.Zero)
            return null;

        return IdealPosition;
    }
    #endregion
}
