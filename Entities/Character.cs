using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Export] public int MaxSpeed;
    [Export] public int Acceleration;
    [Export] public int Friction;
    Vector2 velocity = Vector2.Zero;
    PackedScene bullet;
    [Export] public string BulletPath;
    [Export] public int BulletForce;
    [Export] public InputComponent input;

    public override void _Ready()
    {
        base._Ready();
        bullet = (PackedScene)GD.Load(BulletPath);
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;

        float _delta = (float)delta;

        Vector2 inputDir = input.GetInput();

        //horizontal
        velocity.X = MoveOnAxis(velocity.X, inputDir.X, _delta);

        //vertical
        velocity.Y = MoveOnAxis(velocity.Y, inputDir.Y, _delta);

        if (velocity.Length() > MaxSpeed)
        {
            velocity = velocity.Normalized() * MaxSpeed;
        }

        Velocity = velocity;

        MoveAndSlide();
        
        if (input.GetShootInput())
        {
            Shoot();
        }

    }
    public float MoveOnAxis(float axisSpeed, float axisInput, float delta)
    {

        if (axisInput != 0)
        {
            // we move!

            float actingHorizontalAccel = Acceleration;


            if (
                (System.Math.Abs(axisSpeed) <= (MaxSpeed - (actingHorizontalAccel * System.Math.Abs(axisInput) * delta))) || //if you can accelerate
                (System.Math.Sign(axisInput) != System.Math.Sign(axisSpeed)) // or if you are trying to change directions
                )
            {
                axisSpeed += actingHorizontalAccel * Mathf.Sign(axisInput) * delta;
            }
            else
            {
                // cap speed
                // TODO: maybe soft cap speed? like if ur going over your max speed, just slow down over time until you get back to max speed
                //hsp = maxSpeed * System.Math.Sign(hsp);
                axisSpeed = ApplyAxisFriction(delta, Friction, axisSpeed, MaxSpeed * System.Math.Sign(axisSpeed));
            }
        }
        else
        {
            // we are not moving.
            axisSpeed = ApplyAxisFriction(delta, Friction, axisSpeed, 0);
        }

        return axisSpeed;

    }

    private float ApplyAxisFriction(float delta, float fricAmount, float currentSpeed, float goalSpeed)
    {
        if (System.Math.Abs(currentSpeed) >= System.Math.Abs(goalSpeed) + (fricAmount * delta))
        {
            currentSpeed -= System.Math.Sign(currentSpeed) * fricAmount * delta;
        }
        else
        {
            currentSpeed = goalSpeed;
        }

        return currentSpeed;
    }

    public void Shoot()
    {
        //Shooting
        Bullet b = (Bullet)bullet.Instantiate();

        b.Position = GlobalPosition;
        //b.LookAt(target.Position);

        Vector2 target = input.GetTargetPosition();

        b.speed = BulletForce;
        b.direction = (target - GlobalPosition).Normalized();
        //b.ApplyImpulse(new Vector2(0, 0), new Vector2(BulletForce, 0).Rotated(Rotation));
        GetParent().AddChild(b);
    }
}

