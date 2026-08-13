using Godot;
using System;


public partial class GlobalFunc {
    public static int Mod(int num, int m) => (num % m + m) % m;
}
