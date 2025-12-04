using Godot;

/// <summary>
/// Simple health bar placeholder. You can expand as needed.
/// </summary>
public partial class HealthBar : Node2D
{
    [Export] public Color BarColor = Colors.Green;
    [Export] public Vector2 Size = new Vector2(40, 4);

    public float Ratio = 1f;

    public override void _Draw()
    {
        DrawRect(new Rect2(Vector2.Zero, Size), new Color(0, 0, 0, 0.6f));
        DrawRect(new Rect2(Vector2.Zero, new Vector2(Size.X * Ratio, Size.Y)), BarColor);
    }
}
