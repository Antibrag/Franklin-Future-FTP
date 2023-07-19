using Godot;
using Godot.Collections;
using System;

public static class DataControl
{
    private const string DataSavePath = "user://SavesData.json";
    private const string LevelsDataSavePath = "user://LevelsData.json";

    public static void SaveLevels(Node[] levels)
    {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary dict = new();
        foreach (var level in levels) 
            dict.Add("Level " + (int)level.GetMeta("Id"), (bool)level.GetMeta("IsComplete"));

        file.StoreLine(Json.Stringify(dict));
    }

    public static void LoadLevels(Node[] levels) 
    {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Read);
        if (!FileAccess.FileExists(LevelsDataSavePath)) 
            SaveLevels(levels);
            
        string text = file.GetAsText();

        Dictionary dict = (Dictionary)Json.ParseString(file.GetLine());

        foreach (var level in levels) 
        {
            level.SetMeta("IsComplete", dict["Level " + (int)level.GetMeta("Id")]);
            GD.Print("Data Saved: " + level.GetMeta("Name") + " - " + level.GetMeta("IsComplete"));
        }
    }
}
