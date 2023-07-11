using Godot;
using System;

public struct Level
{
    public string Name { get; set; }
    public int Id { get; set; } 
    public Vector3[] GridSize { get; set; } 
    public int Steps { get; set; } 

    public Level(string name, int id, Vector3[] gridSize, int steps) {
        Name = name;
        Id = id;
        GridSize = gridSize;
        Steps = steps;
    }
}

public partial class LevelControl : Node
{
    public static Level[] Levels = new Level[11];

    public static int CurrentSteps = CurrentLevel.Steps;
    public static Level CurrentLevel = new();

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
