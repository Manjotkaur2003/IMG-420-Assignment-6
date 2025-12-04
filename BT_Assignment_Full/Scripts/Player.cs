using Godot;

/// <summary>
/// WASD / arrow-key controlled player with attack ability.
/// Press SPACE to attack nearby enemies (damages them by 25 HP).
/// </summary>
public partial class Player : CharacterBody2D
{
    [Export] public float MoveSpeed = 200f;
    [Export] public float AttackRange = 80f;
    [Export] public float AttackDamage = 25f;

    public override void _PhysicsProcess(double delta)
    {
        // Movement
        Vector2 input = Vector2.Zero;
        input.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        input.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        if (input.Length() > 1f)
            input = input.Normalized();

        Velocity = input * MoveSpeed;
        MoveAndSlide();

        // Attack with Space key (ui_accept)
        if (Input.IsActionJustPressed("ui_accept"))
        {
            AttackNearbyEnemies();
        }
    }

    private void AttackNearbyEnemies()
    {
        GD.Print("Player attacks!");
        
        // Find all enemies in the scene
        var root = GetTree().CurrentScene;
        foreach (Node child in root.GetChildren())
        {
            // Attack regular enemies
            if (child is Enemy enemy)
            {
                float dist = GlobalPosition.DistanceTo(enemy.GlobalPosition);
                if (dist <= AttackRange)
                {
                    enemy.TakeDamage(AttackDamage);
                    GD.Print($"Hit enemy! Health: {enemy.Health:F0}/{enemy.MaxHealth:F0} ({(enemy.Health/enemy.MaxHealth*100):F0}%)");
                }
            }
            // Attack aggressive enemies
            else if (child is AggressiveEnemy aggEnemy)
            {
                float dist = GlobalPosition.DistanceTo(aggEnemy.GlobalPosition);
                if (dist <= AttackRange)
                {
                    aggEnemy.TakeDamage(AttackDamage);
                    GD.Print($"Hit aggressive enemy! Health: {aggEnemy.Health:F0}/{aggEnemy.MaxHealth:F0}");
                }
            }
        }
    }
}
