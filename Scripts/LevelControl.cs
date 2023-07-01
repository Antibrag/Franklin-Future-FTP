using Godot;
using System;

public partial class LevelControl : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        CurrentLevel.SetLevelName((string)GetMeta("LevelName"));
        CurrentLevel.SetLevelId((int)GetMeta("LevelId"));
		GD.Print(CurrentLevel.GetGridSize());
        GD.Print(CurrentLevel.GetLevelName());
        GD.Print(CurrentLevel.GetLevelId());
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{ 
	//}
}
