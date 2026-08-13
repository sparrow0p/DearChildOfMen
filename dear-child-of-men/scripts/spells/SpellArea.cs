using Godot;
using System;
using System.Collections;


[Tool]
[GlobalClass]
public partial class SpellArea : Area3D {
    private bool _active = true;
    [Export] public bool Active {
        get => _active;
        set {
            _active = value;
            Monitoring = value;
            Monitorable = value;

            if (value)
                EmitSignalActivate();
            else
                EmitSignalDeactivate();
        }
    }
    [Export] private float active_wait_time = -1.0f;
    [Signal] public delegate void ActivateEventHandler();
    [Signal] public delegate void DeactivateEventHandler();
    private const int DefaultCollisionLayer = 0b100000;
    private const int DefaultCollisionMask = 0b10110;
    public Timer ActiveTimer {get; set;} = new();
    public int Id {get; set;}


    public override void _Ready() {
        if (Engine.IsEditorHint())
            return;
        
        Activate += OnActivate;
        if (active_wait_time > 0) {
            AddChild(ActiveTimer);
            ActiveTimer.WaitTime = active_wait_time;
            ActiveTimer.OneShot = true;
            ActiveTimer.ProcessCallback = Timer.TimerProcessCallback.Physics;
            ActiveTimer.Timeout += () => Active = false;
        }

        if (Active)
            OnActivate();
    }


    public override void _EnterTree() {
        base._EnterTree();
        CollisionLayer = DefaultCollisionLayer;
        CollisionMask = DefaultCollisionMask;
    }


    public override bool _PropertyCanRevert(StringName property) {
        if (property == "collision_layer" || property == "collision_mask")
            return true;

        return base._PropertyCanRevert(property);
    }


    public override Variant _PropertyGetRevert(StringName property) {
        if (property == "collision_layer")
            return DefaultCollisionLayer;
        
        if (property == "collision_mask")
            return DefaultCollisionMask;
        
        return base._PropertyGetRevert(property);
    }


    public void OnActivate() {
        if (active_wait_time > 0)
            ActiveTimer.Start();
    }
}
