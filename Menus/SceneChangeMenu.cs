using Godot;
using System;

public partial class SceneChangeMenu : Control
{
    //[Export]
    //public PackedScene gameScene;
    [Export]
    public string ScenePath;
    public void GoToScene()
    {
        //GetTree().ChangeSceneToPacked(gameScene);
        GetTree().ChangeSceneToFile(ScenePath);
    }

    public void ExitGame()
    {
        GetTree().Quit();
    }
}
