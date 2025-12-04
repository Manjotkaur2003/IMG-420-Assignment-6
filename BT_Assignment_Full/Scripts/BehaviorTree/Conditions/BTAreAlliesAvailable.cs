using Godot;

/// <summary>
/// Condition: Success if Enemy can summon allies (AllyScene is set and no allies currently exist).
/// This allows the enemy to call for help when health is low.
/// </summary>
public partial class BTAreAlliesAvailable : BTNode
{
    public override Status Execute()
    {
        // Check if we can summon (AllyScene is configured)
        if (Enemy.AllyScene == null)
            return Status.Failure;
            
        // Only summon if no allies currently exist (avoid spawning infinite allies)
        var allies = Enemy.GetTree().GetNodesInGroup("Allies");
        return allies.Count < 3 ? Status.Success : Status.Failure;
    }
}
