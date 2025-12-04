using Godot;

/// <summary>
/// Action: Enemy summons an Ally for help.
/// EXTRA CREDIT: Includes particle effect when summoning.
/// </summary>
public partial class BTSummonAlly : BTNode
{
    public override Status Execute()
    {
        if (Enemy.AllyScene == null) return Status.Failure;

        Enemy.CurrentState = "SUMMON";

        // Spawn the ally
        Ally ally = Enemy.AllyScene.Instantiate<Ally>();
        ally.GlobalPosition = Enemy.GlobalPosition + new Vector2(30, 0);
        Enemy.GetTree().CurrentScene.AddChild(ally);
        ally.AddToGroup("Allies");

        // EXTRA CREDIT: Create particle effect for summoning
        CreateSummonEffect(Enemy.GlobalPosition);

        GD.Print("Enemy summoned an ally!");

        return Status.Success;
    }

    private void CreateSummonEffect(Vector2 position)
    {
        // Create a simple visual effect using GPUParticles2D
        var particles = new GpuParticles2D();
        particles.GlobalPosition = position;
        particles.Emitting = true;
        particles.Amount = 20;
        particles.Lifetime = 0.5f;
        particles.OneShot = true;
        particles.Explosiveness = 1.0f;

        // Create particle material
        var material = new ParticleProcessMaterial();
        material.EmissionShape = ParticleProcessMaterial.EmissionShapeEnum.Sphere;
        material.EmissionSphereRadius = 20f;
        material.Direction = new Vector3(0, -1, 0);
        material.Spread = 180f;
        material.InitialVelocityMin = 50f;
        material.InitialVelocityMax = 100f;
        material.Gravity = new Vector3(0, 200, 0);
        material.ScaleMin = 3f;
        material.ScaleMax = 6f;
        material.Color = new Color(0.2f, 1f, 0.4f, 1f); // Green color

        particles.ProcessMaterial = material;

        // Add to scene
        Enemy.GetTree().CurrentScene.AddChild(particles);

        // Auto-remove after effect completes
        var timer = Enemy.GetTree().CreateTimer(1.0f);
        timer.Timeout += () => particles.QueueFree();
    }
}
