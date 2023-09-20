using Godot;
using System.Collections.Generic;

public class Level
{
    public string Name { get; set; }
    public int Id { get; set; }
    public Dictionary<string, int> Steps { get; set; }
    public bool IsComplete { get; set; }

    public Level(string name, int id, int steps) {
        Name = name;
        Id = id;

        Steps = new Dictionary<string, int> {
            {"C_LevelSteps", steps},
            {"CurrentSteps", steps}
        };
    }

    public void GetLevelInfo() {
        GD.Print("/--- Current level information ---\\\n");
        GD.Print("Current level name: " + Name);
        GD.Print("Current level id: " + Id);
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

        DataControl.LoadLevelsData(LevelsContainer);
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
        CurrentLevel = new(
            (string)LocalCurrentLevel.GetMeta("Name"),
            (int)LocalCurrentLevel.GetMeta("Id"),
            (int)LocalCurrentLevel.GetMeta("Steps")
        );

        GetNode<HUD>("/root/Main/HUD").ShowLevelName(CurrentLevel.Name);
        LocalCurrentLevel.Position = Vector3.Zero;
    }

    //NOTE!!!
    //Add SaveLevels() on finish player
    public void PlayerFinished(Node3D body) 
    {
        GD.Print("You win!");
        GetNode<Player>("/root/Main/Player").LeaveFromLevel();
    }
}
