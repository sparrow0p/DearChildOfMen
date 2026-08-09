using Godot;
using System;


[Tool]
public partial class TerrainTestCube : Node3D
{
	private MeshInstance3D mesh_instance;


	public override void _Ready() {
		mesh_instance = GetNode<MeshInstance3D>("MeshInstance3D");;

		SurfaceTool st = new SurfaceTool();
		st.Begin(Mesh.PrimitiveType.Triangles);

		Vector3 v1 = new(0, 0, 0);
		Vector3 v2 = new(0, 1, 0);
		Vector3 v3 = new(1, 1, 0);
		Vector3 v4 = new(1, 0, 0);
		Vector3 v5 = new(0, 0, -1);
		Vector3 v6 = new(0, 1, -1);
		Vector3 v7 = new(1, 1, -1);
		Vector3 v8 = new(1, 0, -1);

		add_face(st, v1, v2, v3, v4);
		add_face(st, v4, v3, v7, v8);
		add_face(st, v5, v6, v2, v1);
		add_face(st, v8, v7, v6, v5);
		add_face(st, v2, v6, v7, v3);
		add_face(st, v4, v8, v5, v1);

		st.GenerateNormals();

		mesh_instance.Mesh = st.Commit();
	}


	public override void _Process(double delta) {
	}


	private void add_face(SurfaceTool st, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
		st.AddVertex(v1);
		st.AddVertex(v2);
		st.AddVertex(v3);

		st.AddVertex(v1);
		st.AddVertex(v3);
		st.AddVertex(v4);
	}
}
