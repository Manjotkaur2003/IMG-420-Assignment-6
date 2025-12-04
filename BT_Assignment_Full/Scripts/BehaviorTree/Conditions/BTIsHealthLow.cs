using Godot;

/// <summary>
/// Condition: Success if Enemy health ratio < ThresholdPercent.
/// </summary>
public partial class BTIsHealthLow : BTNode
{
    [Export] public float ThresholdPercent = 0.5f; // 50%

    public override Status Execute()
    {
        float ratio = Enemy.Health / Enemy.MaxHealth;
        return ratio < ThresholdPercent ? Status.Success : Status.Failure;
    }
}
