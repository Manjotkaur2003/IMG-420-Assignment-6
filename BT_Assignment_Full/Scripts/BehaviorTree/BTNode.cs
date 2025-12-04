using Godot;

/// <summary>
/// Base class for all behavior tree nodes.
/// Holds references to Enemy and Player, and defines Execute().
/// </summary>
public abstract partial class BTNode : Node
{
    public enum Status { Success, Failure, Running }

    protected Enemy Enemy;
    protected Player Player;

    public virtual void Initialize(Enemy enemy, Player player)
    {
        Enemy = enemy;
        Player = player;

        // Propagate initialization to BTNode children
        foreach (Node child in GetChildren())
        {
            if (child is BTNode btChild)
                btChild.Initialize(enemy, player);
        }
    }

    public abstract Status Execute();
}
