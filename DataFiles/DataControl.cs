using Godot;
using System;

public static class Data
{
    
}

public class CurrentLevel
{
    private static string _LevelName = null;
    private static int _LevelId = -128;
    private static Vector3[] _GridSize;

    public static void SetLevelName(string newLevelName) { _LevelName = newLevelName; }
    public static void SetLevelId(int newLevelId) { _LevelId = newLevelId; }
    public static void SetLevelGridSize(Vector3[] newGridSize) { _GridSize = newGridSize; } 

    public static string GetLevelName()
    {
        if (String.IsNullOrEmpty(_LevelName)) 
            GD.PushWarning("Level name is not found! Please give a name for your level!");       
        return _LevelName;
    }
    public static int GetLevelId()
    {
        if (_LevelId == -128)
            GD.PushWarning("Level id is not found! Please give id for your level!");
        return _LevelId;
    }
    public static Vector3[] GetLevelGridSize()
    {
        if (_GridSize == null) 
            GD.PushWarning("Grid size is not found! Please update level grid size!");
        return _GridSize;
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
