using Godot;
using Godot.Collections;
using System;

public static class DataControl
{
    private const string DataSavePath = "user://SavesData.json";
    private const string LevelsDataSavePath = "user://LevelsData.json";

    public static void SaveLevels(PackedScene[] scenes)
    {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Write);

        Dictionary dict = new();
        foreach (var level in scenes)
            dict.Add("Level " + (int)level.GetMeta("Id"), (bool)level.GetMeta("IsComplete"));

        file.StoreLine(Json.Stringify(dict));
    }

    private static void SaveLevels() {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Write);
        file.StoreLine(Json.Stringify(new Dictionary()));
    }

    public static void LoadLevels(PackedScene[] scenes) 
    {
        using var file = FileAccess.Open(LevelsDataSavePath, FileAccess.ModeFlags.Read);
        if (!FileAccess.FileExists(LevelsDataSavePath)) SaveLevels();
        string text = file.GetAsText();

        Dictionary dict = (Dictionary)Json.ParseString(file.GetLine());

        for (int i = 0; i < scenes.Length; i++) 
            scenes[i].SetMeta("IsComplete", dict["Level " + i]);
    }
}
