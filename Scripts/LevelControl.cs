using Godot;
using System;

public partial class LevelControl : Node
{
	public override void _Ready()
	{
        CurrentLevel.SetName((string)GetMeta("LevelName"));
        CurrentLevel.SetId((int)GetMeta("LevelId"));
		CurrentLevel.SetGridSize((Vector3[])GetMeta("GridSize"));
    }
}
