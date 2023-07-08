using Godot;
using System;

public static class CurrentLevel
{
    public static string Name { get; set; } = "";
    public static int Id { get; set; } = -1;
    public static Vector3[] GridSize { get; set; } = new Vector3[2];
    public static int Steps { get; set; } = -1;

}

public partial class LevelControl : Node
{
    public static int CurrentSteps = CurrentLevel.Steps;
	public override void _Ready()
	{
        CurrentLevel.Name = (string)GetMeta("Name");
        CurrentLevel.Id = (int)GetMeta("Id");
		CurrentLevel.GridSize = (Vector3[])GetMeta("GridSize");
        CurrentLevel.Steps = (int)GetMeta("Steps");
        GetCurrentLevelInfo(); 
    }

    private static void GetCurrentLevelInfo() {
        GD.Print("/--- Current level information ---\\\n");
        GD.Print("Current level name: " + CurrentLevel.Name);
        GD.Print("Current level id: " + CurrentLevel.Id);
        GD.Print("Current level count steps: " + CurrentLevel.Steps);
        GD.Print("Current level grid size: ");
        foreach (var step in CurrentLevel.GridSize) 
            GD.Print("\t" + step);
    }

    public void PlayerFinished(Node3D body) {
        GD.Print("You win!");
        GetNode<Player>("/root/Main/Player").LeaveFromLevel();
    }
}
