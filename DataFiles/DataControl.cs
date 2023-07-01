using Godot;
using System;

public static class Data
{
    
}

public class CurrentLevel
{
    private static string _LevelName = null;
    private static sbyte _LevelId = -128;

    public static void SetLevelName(string newLevelName) { _LevelName = newLevelName; }
    public static void SetLevelId(sbyte newLevelId) { _LevelId = newLevelId; }

    public static string GetLevelName()
    {
        if (String.IsNullOrEmpty(_LevelName)) 
            GD.Print("Warning! Level name is not found! Please give a name for your level!");       
        return _LevelName;
    }
    public static sbyte GetLevelId()
    {
        if (_LevelId == -128)
            GD.Print("Warning! Level id is not found! Please give id for your level!");
        return _LevelId;
    }
}

public class DataControl
{
    private const string DEB_PATH = "user://DataFiles/SavesData.json";

    public void SaveData()
    {
        
    }

    public void LoadData() 
    {

    }
}
