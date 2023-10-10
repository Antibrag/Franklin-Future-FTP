using Godot;
using System.Collections.Generic;

public struct Level
{
    public Node3D Node { get; set; }
    public Vector3 StartPosition { get; set; }
    public Dictionary<string, int> Steps { get; set; }

    public Level(Node3D node) {
        Node = node;

        StartPosition = node.Position;

        Steps = new Dictionary<string, int> {
            {"C_LevelSteps", (int)Node.GetMeta("Steps")},
            {"CurrentSteps", (int)Node.GetMeta("Steps")}
        };
    }

    public readonly void GetLevelInfo() {
        GD.Print("/--- Current level information ---\\");
        GD.Print($"Current level name: {Node.GetMeta("Name")}");
        GD.Print($"Current level id: {Node.GetMeta("Id")}");
        GD.Print($"Current level count steps: {Steps["C_LevelSteps"]}");
    }
}

public partial class LevelControl : Node
{
    public static Level CurrentLevel { get; set; }

    public static bool ResetCompleteLevels = false;

    public Node3D[] LevelsContainer;

	public override void _Ready()
    {
        LevelsContainer = new Node3D[GetNode<Node>("/root/Main/LevelContainer").GetChildCount()];

        for (int i = 0; i < LevelsContainer.Length; i++)
            LevelsContainer[i] = (Node3D)GetNode<Node>("/root/Main/LevelContainer").GetChild(i);

        if (ResetCompleteLevels)
            DataControl.SaveLevels(LevelsContainer);
            
        DataControl.LoadDataLevels(LevelsContainer);
        LoadLastLevel();
    }

    public void LoadLastLevel()
    {
        foreach (Node3D level in LevelsContainer)
        {
            if ((bool)level.GetMeta("IsComplete") == false) 
            {
                InitCurrentLevel(level);
                break;
            }   
        }
        GetNode<Player>("/root/Main/Player").Show();
    }    

    private void InitCurrentLevel(Node3D LocalCurrentLevel)
    {
        CurrentLevel = new(LocalCurrentLevel);

        GetNode<HUD>("/root/Main/HUD").ShowEleperator((string)CurrentLevel.Node.GetMeta("Name"));
        CurrentLevel.GetLevelInfo();
        LocalCurrentLevel.Position = Vector3.Zero;
    }
}
