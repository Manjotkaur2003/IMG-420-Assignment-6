using Godot;
using System.Collections.Generic;

/// <summary>
/// Base class for composite nodes (Selector, Sequence).
/// Manages a list of BTNode children.
/// </summary>
public abstract partial class BTComposite : BTNode
{
    protected readonly List<BTNode> Children = new();

    public override void Initialize(Enemy enemy, Player player)
    {
        base.Initialize(enemy, player);

        Children.Clear();
        foreach (Node child in GetChildren())
        {
            if (child is BTNode btChild)
                Children.Add(btChild);
        }
    }
}
