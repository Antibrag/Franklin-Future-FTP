using Godot;
using System;

public static class Data
{
    
}

public class CurrentLevel
{
    private static string Name = null;
    private static int Id = -128;
    private static Vector3[] GridSize;
    private static int Steps = -128;

    public static void SetName(string newName) { Name = newName; }
    public static void SetId(int newId) { Id = newId; }
    public static void SetGridSize(Vector3[] newGridSize) { GridSize = newGridSize; } 
    public static void SetSteps(int steps) { Steps = steps; }

    public static string GetName()
    {
        if (String.IsNullOrEmpty(Name)) 
            GD.PushWarning("Level name is not found! Please give a name for your level!");       
        return Name;
    }
    public static int GetId()
    {
        if (Id == -128)
            GD.PushWarning("Level id is not found! Please give id for your level!");
        return Id;
    }
    public static Vector3[] GetGridSize()
    {
        if (GridSize == null) 
            GD.PushWarning("Grid size is not found! Please update level grid size!");
        return GridSize;
    }
    public static int GetSteps() 
    {
        if (Steps == -128)
            GD.PushWarning("Count steps is not found! Please update level count steps!");
        return Steps;
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
