using Godot;
using System;

public partial class Main : Node {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		GetTree().ChangeSceneToNode(GlobalSector.load_sector_at(new Vector2I(1, 2)));
	}
}
