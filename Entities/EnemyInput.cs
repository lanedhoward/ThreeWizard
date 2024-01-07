using Godot;
using System;

public partial class EnemyInput : InputComponent
{
    Vector2 direction;
    double timer = 0;
    [Export] double timerMax = 2;
    public override void _Process(double delta)
    {
        base._Process(delta);

        timer -= delta;

        if (timer <= 0)
        {
            RandomizeDirection();
        }

    }

    public override Vector2 GetInput()
    {
        return direction;
    }

    public void RandomizeDirection()
    {
        timer = GD.RandRange(timerMax / 2, timerMax);
        direction = new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
    }
}