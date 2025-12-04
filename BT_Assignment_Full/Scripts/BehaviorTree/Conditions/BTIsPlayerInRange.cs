using Godot;

/// <summary>
/// Condition: Success if Player is within Range units of Enemy.
/// </summary>
public partial class BTIsPlayerInRange : BTNode
{
    [Export] public float Range = 200f;

    public override Status Execute()
    {
        if (Player == null) return Status.Failure;

        float dist = Enemy.GlobalPosition.DistanceTo(Player.GlobalPosition);
        return dist <= Range ? Status.Success : Status.Failure;
    }
}
