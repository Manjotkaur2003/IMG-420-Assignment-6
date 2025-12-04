using Godot;

/// <summary>
/// Simple ally that moves toward the player when spawned.
/// Counts as "Allies" group for BT conditions.
/// </summary>
public partial class Ally : CharacterBody2D
{
    [Export] public float MoveSpeed = 80f;

    private Player _player;

    public override void _Ready()
    {
        _player = GetTree().Root.GetNodeOrNull<Player>("Main/Player");
        AddToGroup("Allies");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_player == null) return;

        Vector2 dir = (_player.GlobalPosition - GlobalPosition).Normalized();
        Velocity = dir * MoveSpeed;
        MoveAndSlide();
    }
}
