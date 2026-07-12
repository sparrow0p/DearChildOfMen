using Godot;
using System;

[GlobalClass]
public partial class GlobalSector : Node {
	private static readonly String[,] sector_uid_array = {
		{"uid://onb7yptb1yc7"/*0x0*/, "uid://bgro4yy1ik2c6"/*0x1*/, "uid://e8hk5xxq3yip"/*0x2*/},
		{"uid://mwmarg4bf7vo"/*1x0*/, "uid://cq4vup70d5eau"/*1x1*/, "uid://dn0bt76pnng8r"/*1x2*/},
		{"uid://c4c6pbqwd531u"/*2x0*/, "uid://d2rsragij8ugb"/*2x1*/, "uid://6xjl8wpn3hes"/*2x2*/}
	};
	private static readonly Vector2I world_size = new(3, 3);
	private static BaseSector current_sector;
	private static BaseSector[] preloaded_sector_array = [null, null, null, null];


	public static BaseSector load_sector_at(Vector2I coordinates) {
		int x = coordinates.X;
		int y = coordinates.Y;

		current_sector = GD.Load<PackedScene>(sector_uid_array[x, y]).Instantiate<BaseSector>();
		current_sector.load_sector();

		preloaded_sector_array[0] = GD.Load<PackedScene>(sector_uid_array[x, (y-1) % world_size.Y]).Instantiate<BaseSector>();
		preloaded_sector_array[0].ProcessMode = Node.ProcessModeEnum.Disabled;
		preloaded_sector_array[0].preload_sector();

		preloaded_sector_array[1] = GD.Load<PackedScene>(sector_uid_array[x, (y+1) % world_size.Y]).Instantiate<BaseSector>();
		preloaded_sector_array[1].ProcessMode = Node.ProcessModeEnum.Disabled;
		preloaded_sector_array[1].preload_sector();

		if (x-1 >= 0) {
			preloaded_sector_array[2] = GD.Load<PackedScene>(sector_uid_array[x-1, y]).Instantiate<BaseSector>();
			preloaded_sector_array[2].ProcessMode = Node.ProcessModeEnum.Disabled;
			preloaded_sector_array[2].preload_sector();
		}

		if (x+1 < world_size.X) {
			preloaded_sector_array[3] = GD.Load<PackedScene>(sector_uid_array[x+1, y]).Instantiate<BaseSector>();
			preloaded_sector_array[3].ProcessMode = Node.ProcessModeEnum.Disabled;
			preloaded_sector_array[3].preload_sector();
		}

		return current_sector;
	}


	public static void load_sector_from(int from_direction) {
		if (current_sector == null) return;

		int x = current_sector.coordinates.X;
		int y = current_sector.coordinates.Y;
		
	}
}