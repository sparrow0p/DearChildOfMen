using Godot;
using System;


public partial class ShootingStar : BaseSpell {
    [Export(PropertyHint.Range, "0.1,16,0.1,suffix:m")] private float distance = 8.0f;
    [Export] private Curve speed_curve = new();
    [Export] MeshInstance3D star_mesh;
    [Export] MeshInstance3D explosion_mesh;
    private float speed_mult;
    private Timer fly_timer;


    public override void _Ready() {
        fly_timer = SpellAreaArray[0].ActiveTimer;
        speed_mult = distance / AreaUnderCurve(speed_curve) / (float)fly_timer.WaitTime;
        SpellAreaArray[0].Deactivate += explode;
        SpellAreaArray[1].Deactivate += QueueFree;
    }


    public override void _PhysicsProcess(double delta) {
        if (SpellAreaArray[0].Active) {
            float v = speed_curve.Sample((float)Math.Clamp(1 - fly_timer.TimeLeft / fly_timer.WaitTime, 0, 1));
            GlobalPosition += new Vector3(Direction.X, 0, Direction.Y) * speed_mult  * v * (float)delta;
        }
    }


    private void explode() {
        SpellAreaArray[1].Active = true;
        star_mesh.Visible = false;
        explosion_mesh.Visible = true;
    }


    private float AreaUnderCurve(Curve curve, int samples = 100) {
        float dx = 1 / ((float)samples - 1);
        float area = 0.0f;
        float y_old = curve.Sample(0);

        for (int i = 1; i < samples; i++) {
            float x = i * dx;
            float y = curve.Sample(x);

            area += 0.5f * (y_old + y) * dx;

            y_old = y;
        }

        return area;
    }
}
