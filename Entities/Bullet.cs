using Godot;
using System;

public partial class Bullet : Area2D
{
    public float speed;
    public Vector2 direction;
    public int shooterId;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        this.Position += direction * speed * (float)delta;
    }
}
