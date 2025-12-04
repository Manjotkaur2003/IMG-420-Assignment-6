using Godot;

/// <summary>
/// EXTRA CREDIT: Line-of-sight condition.
/// Uses raycasting to check if enemy can actually see the player (not blocked by walls).
/// </summary>
public partial class BTCanSeePlayer : BTNode
{
    [Export] public float Range = 200f;

    public override Status Execute()
    {
        if (Player == null) return Status.Failure;

        float dist = Enemy.GlobalPosition.DistanceTo(Player.GlobalPosition);
        if (dist > Range) return Status.Failure;

        // Raycast to check line of sight
        var spaceState = Enemy.GetWorld2D().DirectSpaceState;
        var query = PhysicsRayQueryParameters2D.Create(
            Enemy.GlobalPosition,
            Player.GlobalPosition,
            1 // Check against layer 1 (walls)
        );
        query.Exclude = new Godot.Collections.Array<Rid> { Enemy.GetRid() };

        var result = spaceState.IntersectRay(query);

        // If ray hits nothing or hits the player, we can see them
        if (result.Count == 0)
        {
            return Status.Success;
        }

        // Check if we hit the player
        if (result.ContainsKey("collider"))
        {
            var collider = result["collider"].As<Node>();
            if (collider is Player)
            {
                return Status.Success;
            }
        }

        // Something is blocking the view
        return Status.Failure;
    }
}

