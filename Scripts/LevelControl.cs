using Godot;
using System;

public partial class LevelControl : Node
{
	public override void _Ready()
	{
        CurrentLevel.SetLevelName((string)GetMeta("LevelName"));
        CurrentLevel.SetLevelId((int)GetMeta("LevelId"));
		CurrentLevel.SetLevelGridSize((Vector3[])GetMeta("GridSize"));
    }
}
