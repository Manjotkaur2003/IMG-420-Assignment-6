using Godot;

/// <summary>
/// Action: Enemy moves toward Player.
/// </summary>
public partial class BTChasePlayer : BTNode
{
    public override Status Execute()
    {
        if (Player == null) return Status.Failure;

        Enemy.CurrentState = "CHASE";

        Vector2 dir = (Player.GlobalPosition - Enemy.GlobalPosition).Normalized();
        Enemy.Velocity = dir * Enemy.MoveSpeed;
        Enemy.MoveAndSlide();

        return Status.Running;
    }
}
