using Godot;
using System;
using System.Collections;
using System.Threading.Tasks;

public partial class Main : Node {
	private Area3D start_click_area;
	private SectorManager sector_manager;


	public override void _Ready() {
		start_click_area = GetNode<Area3D>("Sprite3D/Area3D");
		start_click_area.InputEvent += OnStartClicked;
		sector_manager = GD.Load<PackedScene>("uid://b64vxqqwihcqh").Instantiate<SectorManager>();
	}


    private void OnStartClicked(Node camera, InputEvent @event, Vector3 eventPosition, Vector3 normal, long shapeIdx) {
        if (@event is InputEventMouseButton mb && mb.Pressed && mb.ButtonIndex == MouseButton.Left)
			GetTree().ChangeSceneToNode(sector_manager);
    }
}
