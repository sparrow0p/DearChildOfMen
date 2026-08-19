using Godot;
using System;
using System.Linq;


[GlobalClass]
public partial class PlayerBody3D : CharacterBody3D {
	#region Export Variables
	[Export] private Node3D PlayerMeshNode {get; set;}
	[Export] private Camera3D TpCamera {get; set;}
	[Export] private Node3D TpCameraPivot {get; set;}
	[Export] private Camera3D FpCamera {get; set;}
	[Export] private Node3D FpCameraPivot {get; set;}
	[Export] private SpellBook SpellBook {get; set;}
	#endregion

	#region Other Variables
	private const float MaxWalkSpeed = 9.0f;
	private const float MaxRunSpeed = 90.0f;
	private const float Acceleration = 0.3f;
	private const float Deceleration = 0.35f;
	private const float AngAcceleration = 0.3f;
	public Vector2 Direction {get; set;}
	public Vector2 LastDirection {get; set;} = Vector2.Down;
	private const float scroll_speed = 1.0f;
	private const float mouse_sensitivity = 0.012f;
    #endregion


    #region Godot Functions
	public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseButton mb && mb.Pressed) {
			if (mb.ButtonIndex == MouseButton.Left) {
				if (Input.MouseMode == Input.MouseModeEnum.Visible){
					Input.MouseMode = Input.MouseModeEnum.Captured;
					FpCamera.MakeCurrent();
				} else if (Input.MouseMode == Input.MouseModeEnum.Captured) {
					Input.MouseMode = Input.MouseModeEnum.Visible;
					TpCamera.MakeCurrent();
				}
			}

			if (mb.ButtonIndex == MouseButton.WheelUp)
				TpCamera.Position = TpCamera.Position.MoveToward(Vector3.Back, scroll_speed);

			if (mb.ButtonIndex == MouseButton.WheelDown)
				TpCamera.Position = TpCamera.Position.MoveToward(TpCamera.Position.Normalized() * 2500, scroll_speed);
		}

		if (@event is InputEventMouseMotion mm) {
			if (Input.MouseMode == Input.MouseModeEnum.Captured) {
				PlayerMeshNode.RotateY(-mm.Relative.X * mouse_sensitivity);
				FpCameraPivot.RotateX(mm.Relative.Y * mouse_sensitivity);
				FpCameraPivot.Rotation = new(Mathf.Clamp(FpCameraPivot.Rotation.X, -Mathf.DegToRad(70), Mathf.DegToRad(70)), FpCameraPivot.Rotation.Y, FpCameraPivot.Rotation.Z);
			} else if (Input.MouseMode == Input.MouseModeEnum.Visible && Input.IsActionPressed("middle_mouse")) {
				TpCameraPivot.Rotation = new(TpCameraPivot.Rotation.X - mm.Relative.Y * mouse_sensitivity, TpCameraPivot.Rotation.Y - mm.Relative.X * mouse_sensitivity, 0);
				TpCameraPivot.Rotation = new(Mathf.Clamp(TpCameraPivot.Rotation.X, -Mathf.DegToRad(90), Mathf.DegToRad(90)), TpCameraPivot.Rotation.Y, 0);
			}
		}

		if (@event.IsActionPressed("1")) {
			CastSpell(Combat.SpellEnum.ShootingStar);
		}
    }


	public override void _PhysicsProcess(double delta)
	{
		float velocity_y = Velocity.Y;
		if (!IsOnFloor()) {
			velocity_y += (GetGravity() * (float)delta).Y;
		}

		Direction = Input.GetVector("a", "d", "w", "s").Normalized();
		if (Input.MouseMode == Input.MouseModeEnum.Captured) {
			Direction = Direction.Rotated(-PlayerMeshNode.Rotation.Y + Mathf.Pi);
		} else if (Input.MouseMode == Input.MouseModeEnum.Visible) {
			Direction = Direction.Rotated(-TpCameraPivot.Rotation.Y);
		}

		
		Vector2 velocity_xz = new(Velocity.X, Velocity.Z);
		
		if (Direction != Vector2.Zero) {
			LastDirection = Direction;

			if (Input.IsActionPressed("shift"))
				velocity_xz = velocity_xz.Lerp(Direction * MaxRunSpeed, Acceleration);
			else
				velocity_xz = velocity_xz.Lerp(Direction * MaxWalkSpeed, Acceleration);
			
			if (Input.MouseMode == Input.MouseModeEnum.Visible) {
				float rot = PlayerMeshNode.GlobalRotation.Y;
				rot = Mathf.LerpAngle(rot, -Direction.Angle() + Mathf.Pi / 2, AngAcceleration);
				PlayerMeshNode.GlobalRotation = new(0, rot, 0);
			}
		} else
			velocity_xz = velocity_xz.Lerp(Vector2.Zero, Deceleration);
		
		Velocity = new(velocity_xz.X, velocity_y, velocity_xz.Y);
		MoveAndSlide();
	}
	#endregion


	#region OtherFunctions
	private void CastSpell(Combat.SpellEnum _spell) {
		BaseSpell spell = Combat.SpellScenes[_spell].Instantiate<BaseSpell>();
		GetTree().Root.AddChild(spell);
		spell.Cast(GlobalPosition, LastDirection);
	}
	#endregion
}
