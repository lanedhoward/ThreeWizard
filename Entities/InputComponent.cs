using Godot;
using System;

public partial class InputComponent: Node2D
{
    public virtual Vector2 GetInput()
    {
        return Vector2.Zero;
    }

    public virtual bool GetShootInput()
    {
        return false;
    }

    public virtual Vector2 GetTargetPosition()
    {
        return Vector2.Zero;
    }

    public virtual bool GetParryInput()
    {
        return false;
    }
}
