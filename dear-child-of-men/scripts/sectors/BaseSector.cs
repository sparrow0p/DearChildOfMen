using Godot;
using System;

public partial class BaseSector : Node {
	[Export]
	public Vector2I coordinates;
	public readonly Vector2I sector_size = new(1, 1);


    public override void _Input(InputEvent @event) {
        if (Input.IsActionJustPressed("ui_left")) {
			
		}
    }


	public int load_sector() {
		return 0;
	}


	public int preload_sector() {
		return 0;
	}


	public void unload_sector() {
		return;
	}
}
