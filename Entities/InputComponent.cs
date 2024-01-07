using Godot;
using System;

public partial class InputComponent: Node
{
    public virtual Vector2 GetInput()
    {
        return Vector2.Zero;
    }
}
