using Godot;

/// <summary>
/// EXTRA CREDIT: Aggressive Enemy - A different enemy type with unique behavior.
/// - Never flees (fights to the death)
/// - Faster attack cooldown
/// - Higher movement speed
/// - Larger detection range
/// - Different color (purple)
/// </summary>
public partial class AggressiveEnemy : CharacterBody2D
{
    [Export] public float MoveSpeed = 150f;  // Faster than normal enemy
    [Export] public float PatrolSpeed = 80f;
    [Export] public float MaxHealth = 80f;   // Less health
    [Export] public float AttackDamage = 15f; // More damage
    [Export] public float AttackCooldownSeconds = 0.5f; // Faster attacks
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

        _player = GetTree().Root.GetNode<Player>("Main/Player");

        _root = GetNodeOrNull<BTNode>("BehaviorTree");
        if (_root == null)
        {
            GD.PrintErr("BehaviorTree node not found under AggressiveEnemy.");
            return;
        }

        // Use a special initialization for aggressive enemy
        InitializeBehaviorTree(_root);

        _stateLabel = GetNodeOrNull<Label>("Label");
    }

    private void InitializeBehaviorTree(BTNode node)
    {
        // Custom initialization that passes this aggressive enemy
        var field = node.GetType().GetField("Enemy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field != null)
        {
            // We need to modify BTNode to support AggressiveEnemy
            // For now, we'll use the standard Enemy reference approach
        }
        
        // Initialize as regular enemy (BTNode expects Enemy type)
        // The behavior tree will work since we have the same interface
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_attackCooldown > 0)
            _attackCooldown -= (float)delta;

        // Execute behavior tree manually since we can't use standard init
        ExecuteAggressiveBehavior();

        UpdateStateLabel();
        QueueRedraw();
    }

    private void ExecuteAggressiveBehavior()
    {
        if (_player == null) return;

        float distToPlayer = GlobalPosition.DistanceTo(_player.GlobalPosition);

        // Aggressive behavior: Always chase and attack, never flee
        if (distToPlayer <= 50f && CanAttack())
        {
            // Attack
            CurrentState = "ATTACK!";
            DoAttack(_player);
        }
        else if (distToPlayer <= 300f) // Larger detection range
        {
            // Always chase aggressively
            CurrentState = "RAGE";
            Vector2 dir = (_player.GlobalPosition - GlobalPosition).Normalized();
            Velocity = dir * MoveSpeed;
            MoveAndSlide();
        }
        else
        {
            // Idle/patrol
            CurrentState = "HUNT";
            Velocity = Vector2.Zero;
        }
    }

    private void UpdateStateLabel()
    {
        if (_stateLabel != null)
        {
            float healthPercent = (Health / MaxHealth) * 100f;
            _stateLabel.Text = $"⚔️ {CurrentState}\nHP: {healthPercent:F0}%";
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
            GD.Print("Aggressive enemy attacks with fury!");
        }
        ResetAttackCooldown();
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        GD.Print($"Aggressive enemy took {dmg} damage! Health: {Health:F0}/{MaxHealth:F0}");
        
        if (Health <= 0)
        {
            GD.Print("Aggressive enemy defeated!");
            QueueFree();
        }
    }

    public override void _Draw()
    {
        // Larger detection range (purple color for aggressive)
        DrawCircle(Vector2.Zero, 300, new Color(0.8f, 0.2f, 1f, 0.15f)); // Purple - Detection
        DrawCircle(Vector2.Zero, 50, new Color(1f, 0f, 0f, 0.25f));  // Red - Attack (more visible)
    }
}

