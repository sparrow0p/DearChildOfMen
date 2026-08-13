using Godot;
using System;


public partial class BaseEnemy : Node3D {
    [Export] private float health;
    [Export] private float speed;
    [Export] private Combat.EnemySizeEnum size;
}
