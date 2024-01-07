using Godot;
using System;
using System.Linq;

public partial class EnemyInput : InputComponent
{
    Vector2 direction;
    double timer = 0;
    [Export] double timerMax = 2;

    [Export] Node2D target;

    [Export] Parry parry;

    public override void _Ready()
    {
        base._Ready();
        target = (Node2D)GetTree().CurrentScene.FindChild("Player");
    }

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

    public override bool GetShootInput()
    {
        return (GD.RandRange(0, 100) == 0);
    }

    public override Vector2 GetTargetPosition()
    {
        if (target == null)
        {
            return Vector2.Zero;
        }
        return target.Position;
    }

    public override bool GetParryInput()
    {
        if (GD.RandRange(0, 100) == 0) // random chance to check
        {
            if (parry.GetOverlappingAreas().Where(a => a is Bullet).Count() >= 1) // if theres a bullet in parry range
            {
                return true;
            }
        }

        return false;
    }
}