using Godot;
using System;
using System.Linq;

public partial class Parry : Area2D
{
    AnimatedSprite2D sprite;

    [Export] public Character character;

    [Export] public InputComponent input;

    private double timer;
    private bool timerEnabled;
    [Export] public double timerMax;

    public bool parryEnabled;

    

    public override void _Ready()
    {
        base._Ready();
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        sprite.Visible = false;
        this.Monitorable = false;
        parryEnabled = false;
        this.AreaEntered += Parry_AreaEntered;
    }

    public void StartParry()
    {
        sprite.Visible = true;
        timer = timerMax;
        timerEnabled = true;
        parryEnabled = true;

        foreach (Bullet b in GetOverlappingAreas().Where(a => a is Bullet))
        {
            ParryBullet(b);
        }
    }

    public void EndParry()
    {
        sprite.Visible = false;
        parryEnabled = false;
    }

    private void Parry_AreaEntered(Area2D area)
    {
        if (parryEnabled)
        {
            if (area is Bullet b)
            {
                ParryBullet(b);
            }
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (timerEnabled)
        {
            timer -= delta;
        
            if (timer <= 0)
            {
                timerEnabled = false;
                EndParry();
            }
        }
    }

    public async void ParryBullet(Bullet b)
    {
        b.speed *= 1.15f;

        Vector2 target = input.GetTargetPosition();
        b.direction = (target - b.GlobalPosition).Normalized();

        b.shooterId = character.shooterId;


        //await yield(get_tree().create_timer(0.075), 'timeout');
        GetTree().Paused = true;
        await ToSignal(GetTree().CreateTimer(0.025 + 0.015 * b.timesParried), SceneTreeTimer.SignalName.Timeout);
        GetTree().Paused = false;


        b.timesParried += 1;
    }
}
