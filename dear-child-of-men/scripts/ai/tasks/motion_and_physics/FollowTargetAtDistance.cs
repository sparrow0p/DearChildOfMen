using Godot;
using System;


[Tool]
public partial class FolloTargetAtDistance : BTAction {
    public override string _GenerateName() {
        return "FolloTargetAtDistance";
    }


    public override void _Setup() {
    }


    public override void _Enter() {
    }


    public override void _Exit() {
    }


    public override Status _Tick(double delta) {
        return Status.Success;
    }


    public override string[] _GetConfigurationWarnings() {
        return Array.Empty<string>();
    }
}
