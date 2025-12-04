using Godot;

/// <summary>
/// Action: Enemy performs an attack on Player.
/// </summary>
public partial class BTAttack : BTNode
{
    public override Status Execute()
    {
        if (!Enemy.CanAttack()) return Status.Failure;

        Enemy.CurrentState = "ATTACK";
        Enemy.DoAttack(Player);
        return Status.Success;
    }
}
