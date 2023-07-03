using Godot;
using System;

public partial class LevelControl : Node
{
	public override void _Ready()
	{
        CurrentLevel.SetName((string)GetMeta("Name"));
        CurrentLevel.SetId((int)GetMeta("Id"));
		CurrentLevel.SetGridSize((Vector3[])GetMeta("GridSize"));
        CurrentLevel.SetSteps((int)GetMeta("Steps"));
        GetCurrentLevelInfo(); 
    }

    private static void GetCurrentLevelInfo() {
        GD.Print("/--- Current level information ---\\\n");
        GD.Print("Current level name: " + CurrentLevel.GetName());
        GD.Print("Current level id: " + CurrentLevel.GetId());
        GD.Print("Current level count steps: " + CurrentLevel.GetSteps());
        GD.Print("Current level grid size: ");
        foreach (var step in CurrentLevel.GetGridSize()) 
            GD.Print("\t" + step);
    }
}
