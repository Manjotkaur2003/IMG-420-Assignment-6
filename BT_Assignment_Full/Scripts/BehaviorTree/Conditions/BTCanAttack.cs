using Godot;

/// <summary>
/// Condition: Success if Enemy's attack cooldown is ready.
/// </summary>
public partial class BTCanAttack : BTNode
{
    public override Status Execute()
    {
        return Enemy.CanAttack() ? Status.Success : Status.Failure;
    }
}
