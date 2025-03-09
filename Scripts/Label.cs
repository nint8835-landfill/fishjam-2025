using Godot;

namespace Fishjam.Scripts;

public partial class Label : Timer {
    private bool _fish;
    private TextMesh _mesh;

    public override void _Ready() {
        _mesh = (TextMesh)GetNode<MeshInstance3D>("../MeshInstance3D").Mesh;
        _mesh.Text = "FISH";
    }

    private void _on_timeout() {
        _mesh.Text = _fish ? "JAM" : "FISH";
        _fish = !_fish;
    }
}
