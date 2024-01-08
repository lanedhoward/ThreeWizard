using Godot;
using System;

public partial class GameManager : Node2D
{
    double timer = 0;
    [Export] double timerMax = 2;

    PackedScene enemy;
    [Export] public string EnemyPath;

    [Export] public int XMax, YMax;

    [Export]
    public string ScenePath;
    public void GoToScene()
    {
        //GetTree().ChangeSceneToPacked(gameScene);
        GetTree().ChangeSceneToFile(ScenePath);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        timer -= delta;

        if (timer <= 0)
        {
            timer = GD.RandRange(timerMax / 2, timerMax);
            SpawnEnemy();
        }

    }

    public override void _Ready()
    {
        base._Ready();
        Score.Reflects = 0;
        Score.Kills = 0;
        Score.HighestSpeedReflect = 0;
        timer = GD.RandRange(timerMax / 2, timerMax);
        enemy = (PackedScene)GD.Load(EnemyPath);
    }

    public void SpawnEnemy()
    {
        //Shooting
        Character b = (Character)enemy.Instantiate();

        b.Position = new Vector2(GD.RandRange(0, XMax), GD.RandRange(0, YMax));

        GetParent().AddChild(b);
    }
}
