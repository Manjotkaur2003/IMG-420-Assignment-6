using Godot;

/// <summary>
/// Enemy driven by a Behavior Tree defined as children of its BehaviorTree node.
/// Displays current AI state and health above the enemy.
/// </summary>
public partial class Enemy : CharacterBody2D
{
    [Export] public float MoveSpeed = 100f;
    [Export] public float PatrolSpeed = 60f;
    [Export] public float MaxHealth = 100f;
    [Export] public float AttackDamage = 10f;
    [Export] public float AttackCooldownSeconds = 1.0f;
    [Export] public PackedScene AllyScene;

    public float Health;
    public string CurrentState = "Idle";

    private float _attackCooldown;
    private Player _player;
    private BTNode _root;
    private Label _stateLabel;

    public override void _Ready()
    {
        Health = MaxHealth;
        AddToGroup("Enemies");

        // Get player reference
        _player = GetTree().Root.GetNode<Player>("Main/Player");

        // BehaviorTree node must be a BTSelector / BTNode root
        _root = GetNodeOrNull<BTNode>("BehaviorTree");
        if (_root == null)
        {
            GD.PrintErr("BehaviorTree node not found under Enemy.");
            return;
        }

        _root.Initialize(this, _player);

        // Get state label for displaying current behavior
        _stateLabel = GetNodeOrNull<Label>("Label");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_attackCooldown > 0)
            _attackCooldown -= (float)delta;

        _root?.Execute();

        // Update label to show current state and health
        UpdateStateLabel();

        QueueRedraw();
    }

    private void UpdateStateLabel()
    {
        if (_stateLabel != null)
        {
            float healthPercent = (Health / MaxHealth) * 100f;
            _stateLabel.Text = $"{CurrentState}\nHP: {healthPercent:F0}%";
        }
    }

    public bool CanAttack() => _attackCooldown <= 0f;

    public void ResetAttackCooldown()
    {
        _attackCooldown = AttackCooldownSeconds;
    }

    public void DoAttack(Player player)
    {
        if (player != null)
        {
            GD.Print("Enemy attacks player!");
            CurrentState = "ATTACK";
        }
        ResetAttackCooldown();
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        GD.Print($"Enemy took {dmg} damage! Health: {Health:F0}/{MaxHealth:F0}");
        
        if (Health <= 0)
        {
            GD.Print("Enemy defeated!");
            QueueFree();
        }
    }

    public override void _Draw()
    {
        // Debug visualization circles
        DrawCircle(Vector2.Zero, 200, new Color(1, 1, 0, 0.15f)); // Yellow - Detection range
        DrawCircle(Vector2.Zero, 50, new Color(1, 0, 0, 0.15f));  // Red - Attack range
    }
}
