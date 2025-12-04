using Godot;

/// <summary>
/// Action: Enemy runs directly away from Player.
/// </summary>
public partial class BTFlee : BTNode
{
    public override Status Execute()
    {
        if (Player == null) return Status.Failure;

        Enemy.CurrentState = "FLEE";

        Vector2 dir = (Enemy.GlobalPosition - Player.GlobalPosition).Normalized();
        Enemy.Velocity = dir * Enemy.MoveSpeed;
        Enemy.MoveAndSlide();

        return Status.Running;
    }
}
