using Godot;

namespace Fishjam.Scripts;

public partial class Player : CharacterBody3D {
    private readonly double _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsDouble();

    [Export] public Camera3D Camera;

    [Export] public float JumpSpeed = 5f;

    [Export] public float MouseSensitivity = 0.001f;

    [Export] public float Speed = 10f;

    public override void _PhysicsProcess(double delta) {
        var input = Input.GetVector("left", "right", "forward", "backward");
        var movementDir = Transform.Basis * new Vector3(input.X, 0, input.Y);

        Velocity = Velocity with {
            X = movementDir.X * Speed,
            Y = Velocity.Y - (float)(_gravity * delta),
            Z = movementDir.Z * Speed
        };

        MoveAndSlide();
        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
            Velocity = Velocity with { Y = JumpSpeed };
    }

    public override void _Input(InputEvent @event) {
        if (@event.IsActionPressed("ui_cancel"))
            Input.SetMouseMode(Input.MouseModeEnum.Visible);

        if (@event is not InputEventMouseMotion mouseMotion || Input.MouseMode != Input.MouseModeEnum.Captured) return;

        RotateY(-mouseMotion.Relative.X * MouseSensitivity);
        Camera.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);
        Camera.Rotation = Camera.Rotation with { X = Mathf.Clamp(Camera.Rotation.X, -Mathf.Pi / 2, Mathf.Pi / 2) };
    }

    public override void _Ready() {
        Input.SetMouseMode(Input.MouseModeEnum.Captured);
    }
}
