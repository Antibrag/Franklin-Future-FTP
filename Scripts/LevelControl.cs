using Godot;
using System.Collections.Generic;

public struct Level
{
    public Node3D Node { get; set; }
    public Dictionary<string, int> Steps { get; set; }

    public Level(Node3D node) {
        Node = node;

        Steps = new Dictionary<string, int> {
            {"C_LevelSteps", (int)Node.GetMeta("Steps")},
            {"CurrentSteps", (int)Node.GetMeta("Steps")}
        };
    }

    public void GetLevelInfo() {
        GD.Print("/--- Current level information ---\\\n");
        GD.Print("Current level name: " + Node.GetMeta("Name"));
        GD.Print("Current level id: " + Node.GetMeta("Id"));
        GD.Print("Current level count steps: " + Steps);
        GD.Print("Current level grid size: ");
    }
}

public partial class LevelControl : Node
{
    
    public static Level CurrentLevel { get; set; }

    private Node3D[] LevelsContainer;

	public override void _Ready()
    {
        LevelsContainer = new Node3D[ GetNode<Node>("/root/Main/LevelContainer").GetChildCount()];
        for (int i = 0; i < LevelsContainer.Length; i++)
            LevelsContainer[i] = (Node3D)GetNode<Node>("/root/Main/LevelContainer").GetChild(i);

        DataControl.LoadDataLevels(LevelsContainer);
        LoadLastLevel();
    }

    private void LoadLastLevel()
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
        LocalCurrentLevel.Position = Vector3.Zero;
    }
}
