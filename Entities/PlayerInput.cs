using Godot;
using System;

public partial class PlayerInput : InputComponent
{

    public override Vector2 GetInput()
    {
        int left, right, up, down;
        // Updating Input
        right = Input.IsActionPressed("right") ? 1 : 0;
        left = Input.IsActionPressed("left") ? 1 : 0;
        down = Input.IsActionPressed("down") ? 1 : 0;
        up = Input.IsActionPressed("up") ? 1 : 0;

        Vector2 input = Vector2.Zero;
        input.X = right - left;
        input.Y = down - up;
        return input.Normalized();
    }

    public override bool GetShootInput()
    {
        return Input.IsActionJustPressed("shoot");
    }

    public override Vector2 GetTargetPosition()
    {
        return GetGlobalMousePosition();
    }

    public override bool GetParryInput()
    {
        return Input.IsActionJustPressed("reflect");
    }
}
