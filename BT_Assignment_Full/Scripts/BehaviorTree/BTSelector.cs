using Godot;

/// <summary>
/// Selector: tries children in order until one succeeds.
/// Fails only if all children fail.
/// </summary>
public partial class BTSelector : BTComposite
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
                case Status.Success:
                    _index = 0;
                    return Status.Success;
                case Status.Failure:
                    _index++;
                    break;
            }
        }

        _index = 0;
        return Status.Failure;
    }

    public override void Initialize(Enemy e, Player p)
    {
        base.Initialize(e, p);
        _index = 0;
    }
}
