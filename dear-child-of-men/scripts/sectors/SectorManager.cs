using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SectorManager : Node {
	private static readonly String[,] sector_uid_array = {
		{ "uid://onb7yptb1yc7"/*0x0*/, "uid://bgro4yy1ik2c6"/*0x1*/, "uid://e8hk5xxq3yip"/*0x2*/, "uid://c0dmcbae8pftj"/*0x3*/, ""/*0x4*/, ""/*0x5*/, ""/*0x6*/, "uid://rvxci0uf8k0m"/*0x7*/, "uid://cj7cqopkj5puu"/*0x8*/, "uid://ds0s1ymjw60tk"/*0x9*/, ""/*0x10*/, ""/*0x11*/, ""/*0x12*/, ""/*0x13*/, ""/*0x14*/, ""/*0x15*/ },
		{ "uid://mwmarg4bf7vo"/*1x0*/, "uid://cq4vup70d5eau"/*1x1*/, "uid://dn0bt76pnng8r"/*1x2*/, "uid://cxeje20uove4y"/*1x3*/, ""/*1x4*/, ""/*1x5*/, ""/*1x6*/, "uid://b763iv50mf0x4"/*1x7*/, "uid://cm2ob8emmst08"/*1x8*/, "uid://bd4bkjxbekx3b"/*1x9*/, ""/*1x10*/, ""/*1x11*/, ""/*1x12*/, ""/*1x13*/, ""/*1x14*/, ""/*1x15*/ },
		{ "uid://c4c6pbqwd531u"/*2x0*/, "uid://d2rsragij8ugb"/*2x1*/, "uid://6xjl8wpn3hes"/*2x2*/, "uid://ojo6q63f8ckv"/*2x3*/, ""/*2x4*/, ""/*2x5*/, ""/*2x6*/, "uid://cma1yxll1kusx"/*2x7*/, "uid://052e17clqltn"/*2x8*/, "uid://basay0eji4fjv"/*2x9*/, ""/*2x10*/, ""/*2x11*/, ""/*2x12*/, ""/*2x13*/, ""/*2x14*/, ""/*2x15*/ },
		{ "uid://drstyn2pcer7a"/*3x0*/, "uid://bbonlbhpr3t2m"/*3x1*/, "uid://gvn48fd4lf7j"/*3x2*/, "uid://b5k64d5alvlcb"/*3x3*/, ""/*3x4*/, ""/*3x5*/, ""/*3x6*/, "uid://b76k5te4s04mq"/*3x7*/, "uid://bmhjkqltwhg5h"/*3x8*/, "uid://dvbkm8p4jmsub"/*3x9*/, ""/*3x10*/, ""/*3x11*/, ""/*3x12*/, ""/*3x13*/, ""/*3x14*/, ""/*3x15*/ },
		{ ""/*4x0*/, ""/*4x1*/, ""/*4x2*/, ""/*4x3*/, ""/*4x4*/, ""/*4x5*/, ""/*4x6*/, "uid://bgont372jc5o0"/*4x7*/, "uid://b7xwgq3vj3eig"/*4x8*/, "uid://dd4dkqkwdsrfe"/*4x9*/, ""/*4x10*/, ""/*4x11*/, ""/*4x12*/, ""/*4x13*/, ""/*4x14*/, ""/*4x15*/ },
		{ ""/*5x0*/, ""/*5x1*/, ""/*5x2*/, ""/*5x3*/, ""/*5x4*/, ""/*5x5*/, ""/*5x6*/, "uid://bq844ep6wtdih"/*5x7*/, "uid://hlhthq2gbdl"/*5x8*/, "uid://dn5bpfsa7jj2"/*5x9*/, ""/*5x10*/, ""/*5x11*/, ""/*5x12*/, ""/*5x13*/, ""/*5x14*/, ""/*5x15*/ },
		{ ""/*6x0*/, ""/*6x1*/, ""/*6x2*/, ""/*6x3*/, ""/*6x4*/, ""/*6x5*/, ""/*6x6*/, "uid://bkvksmswuai0f"/*6x7*/, "uid://dyion4w4wnogg"/*6x8*/, "uid://7c8bsyhq66cy"/*6x9*/, ""/*6x10*/, ""/*6x11*/, ""/*6x12*/, ""/*6x13*/, ""/*6x14*/, ""/*6x15*/ },
		{ ""/*7x0*/, ""/*7x1*/, ""/*7x2*/, ""/*7x3*/, ""/*7x4*/, ""/*7x5*/, ""/*7x6*/, "uid://bbsyagsb4ff1p"/*7x7*/, "uid://cbrxuggpj8bki"/*7x8*/, "uid://c1qmip44ttuyh"/*7x9*/, ""/*7x10*/, ""/*7x11*/, ""/*7x12*/, ""/*7x13*/, ""/*7x14*/, ""/*7x15*/ },
		{ ""/*8x0*/, ""/*8x1*/, ""/*8x2*/, ""/*8x3*/, ""/*8x4*/, ""/*8x5*/, ""/*8x6*/, "uid://c07u6iskvee77"/*8x7*/, "uid://0dbf7sv7ix7d"/*8x8*/, "uid://bn0xotva4dvdd"/*8x9*/, ""/*8x10*/, ""/*8x11*/, ""/*8x12*/, ""/*8x13*/, ""/*8x14*/, ""/*8x15*/ },
		{ ""/*9x0*/, ""/*9x1*/, ""/*9x2*/, ""/*9x3*/, ""/*9x4*/, ""/*9x5*/, ""/*9x6*/, "uid://xwav82dtxlcb"/*9x7*/, "uid://c7q62l2y0j5sr"/*9x8*/, "uid://bfmoohbgekts8"/*9x9*/, ""/*9x10*/, ""/*9x11*/, ""/*9x12*/, ""/*9x13*/, ""/*9x14*/, ""/*9x15*/ },
		{ ""/*10x0*/, ""/*10x1*/, ""/*10x2*/, ""/*10x3*/, ""/*10x4*/, ""/*10x5*/, ""/*10x6*/, "uid://d1tgqfd1uiygb"/*10x7*/, "uid://cmyebck1hlo0k"/*10x8*/, "uid://blc45jqfhpunv"/*10x9*/, ""/*10x10*/, ""/*10x11*/, ""/*10x12*/, ""/*10x13*/, ""/*10x14*/, ""/*10x15*/ },
		{ ""/*11x0*/, ""/*11x1*/, ""/*11x2*/, ""/*11x3*/, ""/*11x4*/, ""/*11x5*/, ""/*11x6*/, "uid://du1gmohxgmktp"/*11x7*/, "uid://daf6sk5hkepsl"/*11x8*/, "uid://dhsptv022xpf3"/*11x9*/, ""/*11x10*/, ""/*11x11*/, ""/*11x12*/, ""/*11x13*/, ""/*11x14*/, ""/*11x15*/ },
		{ ""/*12x0*/, ""/*12x1*/, ""/*12x2*/, ""/*12x3*/, ""/*12x4*/, ""/*12x5*/, ""/*12x6*/, "uid://c15l3x4tyqldx"/*12x7*/, "uid://ulnxeddjvb42"/*12x8*/, "uid://ls0ugmk18vjx"/*12x9*/, ""/*12x10*/, ""/*12x11*/, ""/*12x12*/, ""/*12x13*/, ""/*12x14*/, ""/*12x15*/ },
		{ ""/*13x0*/, ""/*13x1*/, ""/*13x2*/, ""/*13x3*/, ""/*13x4*/, ""/*13x5*/, ""/*13x6*/, "uid://d3wgk83ealsv8"/*13x7*/, "uid://074f5htedyim"/*13x8*/, "uid://dexumpbbmllkj"/*13x9*/, ""/*13x10*/, ""/*13x11*/, ""/*13x12*/, ""/*13x13*/, ""/*13x14*/, ""/*13x15*/ },
		{ ""/*14x0*/, ""/*14x1*/, ""/*14x2*/, ""/*14x3*/, ""/*14x4*/, ""/*14x5*/, ""/*14x6*/, "uid://d08a7sfc8e18k"/*14x7*/, "uid://b3a3r43baowxo"/*14x8*/, "uid://cumqwudd475b4"/*14x9*/, ""/*14x10*/, ""/*14x11*/, ""/*14x12*/, ""/*14x13*/, ""/*14x14*/, ""/*14x15*/ },
		{ "uid://b57j0lejx2j8r"/*15x0*/, "uid://bd8jy6dhtl234"/*15x1*/, "uid://0djya31202cw"/*15x2*/, "uid://essygds84s1d"/*15x3*/, "uid://bg4xhoswxbn0o"/*15x4*/, "uid://ddip131fiqw5n"/*15x5*/, "uid://ddltqu4dlfvfq"/*15x6*/, "uid://7bmxaqepeiqv"/*15x7*/, "uid://jksx0peidiy2"/*15x8*/, "uid://dj0vmaydqah4q"/*15x9*/, "uid://dl1u6kfnsqa3r"/*15x10*/, "uid://y74bxmeip2nx"/*15x11*/, "uid://va6sosaftxhm"/*15x12*/, "uid://dt8uudfhiovvu"/*15x13*/, "uid://ts0vqpxdwosd"/*15x14*/, "uid://d0vbhl543mr6c"/*15x15*/ },
		{ "uid://buwhgnabbhnvp"/*16x0*/, "uid://bjhbg6dv15e6"/*16x1*/, "uid://cb0rcjm5pbgkw"/*16x2*/, "uid://bwiqi6882hj0r"/*16x3*/, "uid://nkcnwe53ovub"/*16x4*/, "uid://djhen1dp11rwv"/*16x5*/, "uid://daspigmq4fxgc"/*16x6*/, "uid://cgorthn3n78ir"/*16x7*/, "uid://co3i4nfp85sqb"/*16x8*/, "uid://d2rbf7n3qnj70"/*16x9*/, "uid://bqfjdi7hiuvxt"/*16x10*/, "uid://ra5lgtlgt53w"/*16x11*/, "uid://dj1mtw0w0u105"/*16x12*/, "uid://dto50wxs2dgdi"/*16x13*/, "uid://b02hs8110nmev"/*16x14*/, "uid://bcxnusvbtdei"/*16x15*/ },
		{ "uid://c3bpve7adbusi"/*17x0*/, "uid://d0jx4xtrf48op"/*17x1*/, "uid://bgbw7jlatgds6"/*17x2*/, "uid://1akjaj7f3r83"/*17x3*/, "uid://iiawwlbkoddv"/*17x4*/, "uid://d3dg3o6ixl23h"/*17x5*/, "uid://bd0ndsqw8qpsc"/*17x6*/, "uid://ct27g8krq5e01"/*17x7*/, "uid://bih4g5kyr2jca"/*17x8*/, "uid://d2fmkfuxnbwie"/*17x9*/, "uid://3gr4d6xveb74"/*17x10*/, "uid://bninjpmbli6no"/*17x11*/, "uid://d0xnsf6w2xefj"/*17x12*/, "uid://0pbxj2fagy2x"/*17x13*/, "uid://bfl8ycatkwt71"/*17x14*/, "uid://b3oaqq48jcem7"/*17x15*/ },
		{ ""/*18x0*/, ""/*18x1*/, ""/*18x2*/, ""/*18x3*/, ""/*18x4*/, ""/*18x5*/, ""/*18x6*/, "uid://b4ntegnt0iuat"/*18x7*/, "uid://c8vtwu3fb8akt"/*18x8*/, "uid://b17tesrb5x7mt"/*18x9*/, ""/*18x10*/, ""/*18x11*/, ""/*18x12*/, ""/*18x13*/, ""/*18x14*/, ""/*18x15*/ },
		{ ""/*19x0*/, ""/*19x1*/, ""/*19x2*/, ""/*19x3*/, ""/*19x4*/, ""/*19x5*/, ""/*19x6*/, "uid://mxopfdl4737g"/*19x7*/, "uid://cjic6oh7s31r"/*19x8*/, "uid://cfq7umfeeeu23"/*19x9*/, ""/*19x10*/, ""/*19x11*/, ""/*19x12*/, ""/*19x13*/, ""/*19x14*/, ""/*19x15*/ },
		{ ""/*20x0*/, ""/*20x1*/, ""/*20x2*/, ""/*20x3*/, ""/*20x4*/, ""/*20x5*/, ""/*20x6*/, "uid://dpm18miklecyx"/*20x7*/, "uid://dflvsqly8ptk4"/*20x8*/, "uid://dyqgpdnaumjd6"/*20x9*/, ""/*20x10*/, ""/*20x11*/, ""/*20x12*/, ""/*20x13*/, ""/*20x14*/, ""/*20x15*/ },
		{ ""/*21x0*/, ""/*21x1*/, ""/*21x2*/, ""/*21x3*/, ""/*21x4*/, ""/*21x5*/, ""/*21x6*/, "uid://dt6q54sjc1a0l"/*21x7*/, "uid://bhoi1pqxtqlaq"/*21x8*/, "uid://bv6q6im6y3npw"/*21x9*/, ""/*21x10*/, ""/*21x11*/, ""/*21x12*/, ""/*21x13*/, ""/*21x14*/, ""/*21x15*/ },
		{ ""/*22x0*/, ""/*22x1*/, ""/*22x2*/, ""/*22x3*/, ""/*22x4*/, ""/*22x5*/, ""/*22x6*/, "uid://bioeso0i3xfhf"/*22x7*/, "uid://igw6xkuev8g5"/*22x8*/, "uid://cqgs107akg8ow"/*22x9*/, ""/*22x10*/, ""/*22x11*/, ""/*22x12*/, ""/*22x13*/, ""/*22x14*/, ""/*22x15*/ },
		{ ""/*23x0*/, ""/*23x1*/, ""/*23x2*/, ""/*23x3*/, ""/*23x4*/, ""/*23x5*/, ""/*23x6*/, "uid://cvvlt4bics876"/*23x7*/, "uid://co8faq1usv5h2"/*23x8*/, "uid://cvu3uuf77wd2g"/*23x9*/, ""/*23x10*/, ""/*23x11*/, ""/*23x12*/, ""/*23x13*/, ""/*23x14*/, ""/*23x15*/ },
		{ ""/*24x0*/, ""/*24x1*/, ""/*24x2*/, ""/*24x3*/, ""/*24x4*/, ""/*24x5*/, ""/*24x6*/, "uid://c67bd8ukxu5s6"/*24x7*/, "uid://b1rsjwgklhrgd"/*24x8*/, "uid://dgrfdkggibjyg"/*24x9*/, ""/*24x10*/, ""/*24x11*/, ""/*24x12*/, ""/*24x13*/, ""/*24x14*/, ""/*24x15*/ },
		{ ""/*25x0*/, ""/*25x1*/, ""/*25x2*/, ""/*25x3*/, ""/*25x4*/, ""/*25x5*/, ""/*25x6*/, "uid://dhskbuxpheykk"/*25x7*/, "uid://sm5rhtnjsdx0"/*25x8*/, "uid://5o12gne087n"/*25x9*/, ""/*25x10*/, ""/*25x11*/, ""/*25x12*/, ""/*25x13*/, ""/*25x14*/, ""/*25x15*/ },
		{ ""/*26x0*/, ""/*26x1*/, ""/*26x2*/, ""/*26x3*/, ""/*26x4*/, ""/*26x5*/, ""/*26x6*/, "uid://cvixcuyj30aga"/*26x7*/, "uid://4ydclykgfnbr"/*26x8*/, "uid://c16qatnhuirf"/*26x9*/, ""/*26x10*/, ""/*26x11*/, ""/*26x12*/, ""/*26x13*/, ""/*26x14*/, ""/*26x15*/ },
		{ ""/*27x0*/, ""/*27x1*/, ""/*27x2*/, ""/*27x3*/, ""/*27x4*/, ""/*27x5*/, ""/*27x6*/, "uid://c0tbc6yq1vpft"/*27x7*/, "uid://b0bvr7nxer0ti"/*27x8*/, "uid://dquk23yfcryd1"/*27x9*/, ""/*27x10*/, ""/*27x11*/, ""/*27x12*/, ""/*27x13*/, ""/*27x14*/, ""/*27x15*/ },
		{ ""/*28x0*/, ""/*28x1*/, ""/*28x2*/, ""/*28x3*/, ""/*28x4*/, ""/*28x5*/, ""/*28x6*/, "uid://46i8sdsfc0dg"/*28x7*/, "uid://rmpny8tybyrv"/*28x8*/, "uid://bfrvx0lkhopo1"/*28x9*/, ""/*28x10*/, ""/*28x11*/, ""/*28x12*/, ""/*28x13*/, ""/*28x14*/, ""/*28x15*/ },
		{ ""/*29x0*/, ""/*29x1*/, ""/*29x2*/, ""/*29x3*/, ""/*29x4*/, ""/*29x5*/, ""/*29x6*/, "uid://drhodhyvn4spb"/*29x7*/, "uid://b6dkngteuxt1n"/*29x8*/, "uid://dvhjuhhul8qh3"/*29x9*/, ""/*29x10*/, ""/*29x11*/, ""/*29x12*/, ""/*29x13*/, ""/*29x14*/, ""/*29x15*/ },
		{ ""/*30x0*/, ""/*30x1*/, ""/*30x2*/, ""/*30x3*/, ""/*30x4*/, ""/*30x5*/, ""/*30x6*/, "uid://ulfw7pp13tp7"/*30x7*/, "uid://dn4s65ff6i5ir"/*30x8*/, "uid://orst3u1js8a1"/*30x9*/, ""/*30x10*/, ""/*30x11*/, ""/*30x12*/, ""/*30x13*/, ""/*30x14*/, ""/*30x15*/ },
		{ ""/*31x0*/, ""/*31x1*/, ""/*31x2*/, ""/*31x3*/, ""/*31x4*/, ""/*31x5*/, ""/*31x6*/, "uid://dmt2q73xsw7u1"/*31x7*/, "uid://c7dypevvpom60"/*31x8*/, "uid://b43ampuwifxcj"/*31x9*/, ""/*31x10*/, ""/*31x11*/, ""/*31x12*/, ""/*31x13*/, ""/*31x14*/, ""/*31x15*/ }
	};
	private static readonly Vector2I world_size = new(32, 16);
	private SceneTree Tree {get; set;}
	public const float sector_diameter = 100;
	private BaseSector current_sector;
	private Vector2I current_sector_coordinates;
	private Vector2I global_coordinates;
	private readonly BaseSector[] preloaded_sector_array = new BaseSector[8];
	private readonly string[] preload_path_array = new string[8];
	private readonly Vector3[] preload_position_array = new Vector3[8];
	private readonly HashSet<string> is_sector_loading_set = new();
	[Export] private CharacterBody3D player;
	[Export] private DirectionalLight3D the_sun;
	[Export] private WorldEnvironment environment;
	[Export] private Node3D the_world;
	[Export] private RayCast3D world_ray_cast;


    public override void _Ready() {
		GlobalVar.TheSun = the_sun;
		GlobalVar.TheWorld = the_world;
		GlobalVar.SectorManager = this;
		Tree = GetTree();
		player = GlobalVar.Player;

		load_sector_at(new(16, 8));
    }


    public override void _Process(double delta) {
		check_loading_sectors();
    }


    public override void _PhysicsProcess(double delta) {
        rotate_neighbouring_sectors();
		rotate_world();
		rotate_sun();
    }


	public bool load_sector_at(Vector2I coordinates) {
		if (current_sector != null) current_sector.unload_sector();
		foreach (BaseSector sector in preloaded_sector_array)
			if (sector != null) sector.unload_sector();
		
		Array.Clear(preloaded_sector_array, 0, preloaded_sector_array.Length);

		int x = coordinates.X;
		int y = coordinates.Y;

		if (!can_load_sector(x, y))
			return false;

		current_sector = GD.Load<PackedScene>(sector_uid_array[x, y]).Instantiate<BaseSector>();
		current_sector.preload_sector();
		current_sector.load_sector();
		current_sector_coordinates = new(x, y);
		global_coordinates = new(0, 0);

		AddChild(current_sector);

		foreach (int i in Enumerable.Range(0, 8))
			preload_neighbour_sector(x, y, i);

		return true;
	}


	public bool move_to_sector(String direction) {
		if (current_sector == null) return false;

		BaseSector previous_sector = current_sector;
		int previous_sector_idx = 0;
		int previous_sector_x = current_sector_coordinates.X;
		int previous_sector_y = current_sector_coordinates.Y;
		int target_index = -1;
        Vector2I target_coordinates = new(0, 0);
        Vector2I global_coordinates_delta = new(0, 0);
		int[] sectors_to_unload = new int[3];
		int[,] sectors_to_move = new int[4,2];
		int[] sectors_to_preload = new int[3];
		
		switch (direction) {
			case "left":
				target_index = 3;
                target_coordinates = new(previous_sector_x, GlobalFunc.Mod(previous_sector_y-1, world_size.Y));
                global_coordinates_delta = new(-1, 0);
                sectors_to_unload = [2, 4, 7];
                sectors_to_move = new int[,] {{1, 2}, {0, 1}, {6, 7}, {5, 6}};
                sectors_to_preload = [0, 3, 5];
                previous_sector_idx = 4;
                break;

			case "right":
				target_index = 4;
                target_coordinates = new(previous_sector_x, GlobalFunc.Mod(previous_sector_y+1, world_size.Y));
                global_coordinates_delta = new(1, 0);
                sectors_to_unload = [0, 3, 5];
                sectors_to_move = new int[,] {{1, 0}, {2, 1}, {6, 5}, {7, 6}};
                sectors_to_preload = [2, 4, 7];
                previous_sector_idx = 3;
                break;

			case "up":
				target_index = 1;
                target_coordinates = new(previous_sector_x-1, previous_sector_y);
                global_coordinates_delta = new(0, -1);
                sectors_to_unload = [5, 6, 7];
                sectors_to_move = new int[,] {{3, 5}, {0, 3}, {4, 7}, {2, 4}};
                sectors_to_preload = [0, 1, 2];
                previous_sector_idx = 6;
                break;

			case "down":
				target_index = 6;
                target_coordinates = new(previous_sector_x+1, previous_sector_y);
                global_coordinates_delta = new(0, 1);
                sectors_to_unload = [0, 1, 2];
                sectors_to_move = new int[,] {{3, 0}, {5, 3}, {4, 2}, {7, 4}};
                sectors_to_preload = [5, 6, 7];
                previous_sector_idx = 1;
                break;
		}
		
		if (!can_load_sector(target_coordinates.X, target_coordinates.Y))
            return false;
		
		current_sector = ensure_sector_loaded(target_coordinates.X, target_coordinates.Y, target_index);
		current_sector.load_sector();
        current_sector_coordinates = target_coordinates;
        global_coordinates += global_coordinates_delta;
		current_sector.GlobalPosition = new(((Vector2)global_coordinates * sector_diameter).X, current_sector.GlobalPosition.Y, ((Vector2)global_coordinates * sector_diameter).Y);
		preloaded_sector_array[target_index] = null;
        preload_path_array[target_index] = null;

		foreach (int i in sectors_to_unload) {
            preloaded_sector_array[i]?.unload_sector();
            preloaded_sector_array[i] = null;

			string path = preload_path_array[i];
			if (path != null) {
				if (is_sector_loading_set.Contains(path)) {
					is_sector_loading_set.Remove(path);
					preload_path_array[i] = null;
				}
			}
        }

        preloaded_sector_array[previous_sector_idx] = previous_sector;
        previous_sector.ProcessMode = Node.ProcessModeEnum.Disabled;

		for (int i = 0; i < sectors_to_move.GetLength(0); i++) {
			preloaded_sector_array[sectors_to_move[i, 1]] = preloaded_sector_array[sectors_to_move[i, 0]];
			preloaded_sector_array[sectors_to_move[i, 0]] = null;
		}

		foreach (int i in sectors_to_preload)
			preload_neighbour_sector(current_sector_coordinates.X, current_sector_coordinates.Y, i);

		return true;
	}


	private void preload_neighbour_sector(int x, int y, int index) {
		(int cx, int cy, float px, float pz) = get_neighbour_sector_offset(x, y, index);

		if (!can_load_sector(cx, cy)) {
			preloaded_sector_array[index] = null;
            preload_path_array[index] = null;
            return;
		}

		string path = sector_uid_array[cx, cy];
		preload_path_array[index] = path;
		preload_position_array[index] = new(global_coordinates.X * sector_diameter + px, 0, global_coordinates.Y * sector_diameter + pz);

		if (preloaded_sector_array[index] != null)
            return;

        if (is_sector_loading_set.Contains(path))
            return;
					
		is_sector_loading_set.Add(path);
		ResourceLoader.LoadThreadedRequest(path);
	}


	private static bool can_load_sector(int x, int y) {
		if (x < 0) return false;
		if (x >= world_size.X) return false;
		if (!ResourceLoader.Exists(sector_uid_array[x, GlobalFunc.Mod(y, world_size.Y)], "PackedScene")) return false;

		return true;
	}


	private static (int cx, int cy, float px, float pz) get_neighbour_sector_offset(int x, int y, int index) {
		return index switch {
			0 => (x-1, GlobalFunc.Mod(y-1, world_size.Y), -sector_diameter, -sector_diameter),
			1 => (x-1, GlobalFunc.Mod(y, world_size.Y), 0, -sector_diameter),
			2 => (x-1, GlobalFunc.Mod(y+1, world_size.Y), sector_diameter, -sector_diameter),
			3 => (x, GlobalFunc.Mod(y-1, world_size.Y), -sector_diameter, 0),
			4 => (x, GlobalFunc.Mod(y+1, world_size.Y), sector_diameter, 0),
			5 => (x+1, GlobalFunc.Mod(y-1, world_size.Y), -sector_diameter, sector_diameter),
			6 => (x+1, GlobalFunc.Mod(y, world_size.Y), 0, sector_diameter),
			7 => (x+1, GlobalFunc.Mod(y+1, world_size.Y), sector_diameter, sector_diameter),
			_ => (x, GlobalFunc.Mod(y, world_size.Y), 0, 0)
		};
	}


	private BaseSector ensure_sector_loaded(int x, int y, int index) {
        string path = sector_uid_array[x, y];

        if (preloaded_sector_array[index] != null)
            return preloaded_sector_array[index];

        if (!is_sector_loading_set.Contains(path))
            ResourceLoader.LoadThreadedRequest(path);

        PackedScene res = ResourceLoader.LoadThreadedGet(path) as PackedScene;
        is_sector_loading_set.Remove(path);

        if (res == null)
            throw new Exception($"Failed to load sector: {path}");

        BaseSector sector = res.Instantiate<BaseSector>();
        AddChild(sector);
        sector.GlobalPosition = new Vector3(global_coordinates.X * sector_diameter, 0, global_coordinates.Y * sector_diameter);
        return sector;
    }


	private void print_preloaded_sector_array() {
		for (int i = 0; i < 8; i++) {
			if (preloaded_sector_array[i] != null)
				GD.Print($"{preloaded_sector_array[i]} {i}");
			else
				GD.Print("null");
		}
		GD.Print();
	}


	private void check_loading_sectors() {
		for (int i = 0; i < 8; i++) {
			string path = preload_path_array[i];

			if (path != null) {
				if (is_sector_loading_set.Contains(path)) {
					if (ResourceLoader.LoadThreadedGetStatus(path) == ResourceLoader.ThreadLoadStatus.Loaded) {
						is_sector_loading_set.Remove(path);
						preload_path_array[i] = null;

						preloaded_sector_array[i] = (ResourceLoader.LoadThreadedGet(path) as PackedScene).Instantiate<BaseSector>();
						AddChild(preloaded_sector_array[i]);
						preloaded_sector_array[i].GlobalPosition = preload_position_array[i];
						preloaded_sector_array[i].preload_sector();
					}
				}
			}
		}
	}


	private void rotate_neighbouring_sectors() {
		if (player == null || current_sector == null)
			return;

		Vector3 cs_gp = current_sector.GlobalPosition;
		float left_pivot = cs_gp.X - sector_diameter/2;
		float right_pivot = cs_gp.X + sector_diameter/2;
			
		foreach (int i in new int[] {0, 3, 5}) {
			BaseSector sector = preloaded_sector_array[i];
			if (sector != null) {
				Vector3 axis = new(0, 0, 1);
				float angle = (player.GlobalPosition.X - left_pivot) / sector_diameter * Mathf.Pi / 8;
				Vector3 offset = new(-sector_diameter/2, 0, 0);

				offset = offset.Rotated(axis, angle);
				sector.GlobalPosition = new(left_pivot + offset.X, offset.Y, sector.GlobalPosition.Z);
				sector.Rotation = new(0, 0, angle);
			}	
		}

		foreach (int i in new int[] {2, 4, 7}) {
			BaseSector sector = preloaded_sector_array[i];
			if (sector != null) {
				Vector3 axis = new(0, 0, 1);
				float angle = (player.GlobalPosition.X - right_pivot) / sector_diameter * Mathf.Pi / 8;
				Vector3 offset = new(sector_diameter/2, 0, 0);

				offset = offset.Rotated(axis, angle);
				sector.GlobalPosition = new(right_pivot + offset.X, offset.Y, sector.GlobalPosition.Z);
				sector.Rotation = new(0, 0, angle);
			}	
		}
	}


	private void rotate_world() {
		world_ray_cast.ForceRaycastUpdate();
		float delta_height = 0;

		if (world_ray_cast.IsColliding())
            delta_height = player.GlobalPosition.Y - world_ray_cast.GetCollisionPoint().Y - 5;

		the_world.GlobalPosition = new(player.GlobalPosition.X, the_world.GlobalPosition.Y + delta_height, the_world.GlobalPosition.Z);
		the_world.Rotation = new(-Mathf.Pi/2 + Mathf.Pow(player.GlobalPosition.Z / 1600, 3) * Mathf.Pi / 4, -(player.GlobalPosition.X + 50) * 2 * Mathf.Pi / 1600, 0);
	}
	

	private void rotate_sun() {
		the_sun.Rotation = the_world.Rotation + new Vector3(0, 0, 0);
		environment.Environment.SkyRotation = new(0, 0, Mathf.Pi/2 - the_sun.Rotation.Y);
	}
}
