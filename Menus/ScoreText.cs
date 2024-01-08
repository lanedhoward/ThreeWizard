using Godot;
using System;

public partial class ScoreText : Label
{
    public override void _Ready()
    {
        base._Ready();
        Text = 
$@"Kills: {Score.Kills}
Reflects: {Score.Reflects}
Highest Speed Reflect: {Mathf.Round(Score.HighestSpeedReflect)}";
    }
}
