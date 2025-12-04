using Godot;

/// <summary>
/// Sequence: runs children in order; fails if any child fails.
/// Succeeds only if all children succeed.
/// </summary>
public partial class BTSequence : BTComposite
{
    private int _index = 0;

    public override Status Execute()
    {
        if (Children.Count == 0)
            return Status.Failure;

        while (_index < Children.Count)
        {
            var result = Children[_index].Execute();

            switch (result)
            {
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    _index = 0;
                    return Status.Failure;
                case Status.Success:
                    _index++;
                    break;
            }
        }

        _index = 0;
        return Status.Success;
    }

    public override void Initialize(Enemy e, Player p)
    {
        base.Initialize(e, p);
        _index = 0;
    }
}
