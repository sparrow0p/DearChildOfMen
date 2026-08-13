using Godot;
using System;

public partial class BaseSector : Node3D {
	private SectorManager sector_manager;
	private CharacterBody3D player;
	private float sector_diameter;


    public override void _Ready() {
        player = GlobalVar.Player;
		sector_manager = GlobalVar.SectorManager;
		sector_diameter = SectorManager.sector_diameter;
    }


    public override void _Input(InputEvent @event) {
		if (Input.IsActionJustPressed("ui_left")) {
			sector_manager.move_to_sector("left");
		} else if(Input.IsActionJustPressed("ui_right")) {
			sector_manager.move_to_sector("right");
		} else if(Input.IsActionJustPressed("ui_up")) {
			sector_manager.move_to_sector("up");
		} else if(Input.IsActionJustPressed("ui_down")) {
			sector_manager.move_to_sector("down");
		}
    }


    public override void _Process(double delta) {
		if (player != null) {
			Vector3 local_pos_v3 = player.GlobalPosition - GlobalPosition;
			Vector2 local_pos = new(local_pos_v3.X, local_pos_v3.Z);

			if (local_pos.X < -sector_diameter / 2) {
				if (!sector_manager.move_to_sector("left"))
					player.GlobalPosition += new Vector3(1, 0, 0);
			} else if(local_pos.X > sector_diameter / 2) {
				if (!sector_manager.move_to_sector("right"))
					player.GlobalPosition += new Vector3(-1, 0, 0);
			} else if(local_pos.Y < -sector_diameter / 2) {
				if (!sector_manager.move_to_sector("up"))
					player.GlobalPosition += new Vector3(0, 0, 1);
			} else if(local_pos.Y > sector_diameter / 2) {
				if (!sector_manager.move_to_sector("down"))
					player.GlobalPosition += new Vector3(0, 0, -1);
			}
		}
    }


	public int preload_sector() {
		//GD.Print($"{Name}, preload");
		ProcessMode = Node.ProcessModeEnum.Disabled;
		return 0;
	}


	public int load_sector() {
		//GD.Print($"{Name}, load");
		ProcessMode = Node.ProcessModeEnum.Inherit;
		return 0;
	}


	public void unload_sector() {
		//GD.Print($"{Name}, unload");
		QueueFree();
	}
}
