 using Godot;
using System;

public partial class Player : CharacterBody3D {
	private const float MaxWalkSpeed = 9.0f;
	private const float MaxRunSpeed = 90.0f;
	private const float Acceleration = 0.3f;
	private const float Deceleration = 0.35f;
	private const float AngAcceleration = 0.3f;
	private Vector2 direction;
	[Export] private Node3D player_mesh;
	[Export] private Camera3D tp_camera;
	[Export] private Node3D tp_camera_pivot;
	[Export] private Camera3D fp_camera;
	[Export] private Node3D fp_camera_pivot;
	private const float scroll_speed = 1.0f;
	private const float mouse_sensitivity = 0.012f;


    public override void _Ready() {
		GlobalVar.Player = this;
    }


    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseButton mb && mb.Pressed) {
			if (mb.ButtonIndex == MouseButton.Left) {
				if (Input.MouseMode == Input.MouseModeEnum.Visible){
					Input.MouseMode = Input.MouseModeEnum.Captured;
					fp_camera.MakeCurrent();
				} else if (Input.MouseMode == Input.MouseModeEnum.Captured) {
					Input.MouseMode = Input.MouseModeEnum.Visible;
					tp_camera.MakeCurrent();
				}
			}

			if (mb.ButtonIndex == MouseButton.WheelUp)
				tp_camera.Position = tp_camera.Position.MoveToward(Vector3.Back, scroll_speed);

			if (mb.ButtonIndex == MouseButton.WheelDown)
				tp_camera.Position = tp_camera.Position.MoveToward(tp_camera.Position.Normalized() * 2500, scroll_speed);
		}

		if (@event is InputEventMouseMotion mm) {
			if (Input.MouseMode == Input.MouseModeEnum.Captured) {
				player_mesh.RotateY(-mm.Relative.X * mouse_sensitivity);
				fp_camera_pivot.RotateX(mm.Relative.Y * mouse_sensitivity);
				fp_camera_pivot.Rotation = new(Mathf.Clamp(fp_camera_pivot.Rotation.X, -Mathf.DegToRad(70), Mathf.DegToRad(70)), fp_camera_pivot.Rotation.Y, fp_camera_pivot.Rotation.Z);
			} else if (Input.MouseMode == Input.MouseModeEnum.Visible && Input.IsActionPressed("middle_mouse")) {
				tp_camera_pivot.Rotation = new(tp_camera_pivot.Rotation.X - mm.Relative.Y * mouse_sensitivity, tp_camera_pivot.Rotation.Y - mm.Relative.X * mouse_sensitivity, 0);
				tp_camera_pivot.Rotation = new(Mathf.Clamp(tp_camera_pivot.Rotation.X, -Mathf.DegToRad(90), Mathf.DegToRad(90)), tp_camera_pivot.Rotation.Y, 0);
			}
		}
    }


	public override void _PhysicsProcess(double delta)
	{
		float velocity_y = Velocity.Y;
		if (!IsOnFloor()) {
			velocity_y += (GetGravity() * (float)delta).Y;
		}

		direction = Input.GetVector("a", "d", "w", "s").Normalized();
		if (Input.MouseMode == Input.MouseModeEnum.Captured) {
			direction = direction.Rotated(-player_mesh.Rotation.Y + Mathf.Pi);
		} else if (Input.MouseMode == Input.MouseModeEnum.Visible) {
			direction = direction.Rotated(-tp_camera_pivot.Rotation.Y);
		}


		
		Vector2 velocity_xz = new(Velocity.X, Velocity.Z);
		
		if (direction != Vector2.Zero) {
			if (Input.IsActionPressed("shift"))
				velocity_xz = velocity_xz.Lerp(direction * MaxRunSpeed, Acceleration);
			else
				velocity_xz = velocity_xz.Lerp(direction * MaxWalkSpeed, Acceleration);
			
			if (Input.MouseMode == Input.MouseModeEnum.Visible) {
				float rot = player_mesh.GlobalRotation.Y;
				rot = Mathf.LerpAngle(rot, -direction.Angle() + Mathf.Pi / 2, AngAcceleration);
				player_mesh.GlobalRotation = new(0, rot, 0);
			}
		} else
			velocity_xz = velocity_xz.Lerp(Vector2.Zero, Deceleration);
		
		Velocity = new(velocity_xz.X, velocity_y, velocity_xz.Y);
		MoveAndSlide();
	}
}
