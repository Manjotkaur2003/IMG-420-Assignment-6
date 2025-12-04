using Godot;

/// <summary>
/// Condition: Success if Enemy health ratio < ThresholdPercent.
/// </summary>
public partial class BTIsHealthCritical : BTNode
{
    [Export] public float ThresholdPercent = 0.2f; // 20%

    public override Status Execute()
    {
        float ratio = Enemy.Health / Enemy.MaxHealth;
        return ratio < ThresholdPercent ? Status.Success : Status.Failure;
    }
}
