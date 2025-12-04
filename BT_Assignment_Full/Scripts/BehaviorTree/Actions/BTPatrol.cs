using Godot;

/// <summary>
/// Action: Enemy patrols between editor-assigned patrol points.
/// </summary>
public partial class BTPatrol : BTNode
{
    [Export] public NodePath[] PatrolPointsPaths;

    private Node2D[] _points;
    private int _index = 0;

    public override void Initialize(Enemy e, Player p)
    {
        base.Initialize(e, p);

        if (PatrolPointsPaths == null) return;

        _points = new Node2D[PatrolPointsPaths.Length];
        for (int i = 0; i < PatrolPointsPaths.Length; i++)
        {
            if (PatrolPointsPaths[i] != null && !PatrolPointsPaths[i].IsEmpty)
                _points[i] = Enemy.GetNode<Node2D>(PatrolPointsPaths[i]);
        }
    }

    public override Status Execute()
    {
        if (_points == null || _points.Length == 0)
            return Status.Failure;

        Node2D target = _points[_index];
        if (target == null) return Status.Failure;

        Enemy.CurrentState = "PATROL";

        float dist = Enemy.GlobalPosition.DistanceTo(target.GlobalPosition);

        if (dist < 10f)
        {
            _index = (_index + 1) % _points.Length;
            return Status.Success;
        }

        Vector2 dir = (target.GlobalPosition - Enemy.GlobalPosition).Normalized();
        Enemy.Velocity = dir * Enemy.PatrolSpeed;
        Enemy.MoveAndSlide();

        return Status.Running;
    }
}
