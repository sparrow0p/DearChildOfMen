using Godot;
using System;
using System.Reflection.Metadata.Ecma335;


public partial class GlobalVar {
    public static PlayerBody3D Player {get; set;}
    public static DirectionalLight3D TheSun {get; set;}
    public static Node3D TheWorld {get; set;}
    public static WorldEnvironment MainEnvironment {get; set;}
    public static SectorManager SectorManager {get; set;}
}
