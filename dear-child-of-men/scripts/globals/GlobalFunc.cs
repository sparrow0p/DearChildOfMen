using Godot;
using System;

[GlobalClass]
public partial class GlobalFunc : Node {
    public static int Mod(int num, int m) => (num % m + m) % m;
}
