using Godot;
using System;

public class Level
{
    public string Name { get; set; }
    public int Id { get; set; }
    public Vector3[] GridSize { get; set; }
    public int Steps { get; set; }
    public int CurrentSteps { get; set; }
    public Node3D[] Objects { get; set; }
    public bool IsComplete { get; set; }

    public Level(string name, int id, Vector3[] gridSize, int steps, Node3D[] objects) {
        Name = name;
        Id = id;
        GridSize = gridSize;
        Steps = steps;
        Objects = objects;
    }

    public void GetLevelInfo() {
        GD.Print("/--- Current level information ---\\\n");
        GD.Print("Current level name: " + Name);
        GD.Print("Current level id: " + Id);
        GD.Print("Current level count steps: " + Steps);
        GD.Print("Current level grid size: ");
        foreach (var step in GridSize) 
            GD.Print("\t" + step);
    }
}

public partial class LevelControl : Node
{
    public static Level CurrentLevel;
    public static int CurrentSteps;
    private Node3D[] LevelsContainer;
    private Node3D LevelObjectsNode;

	public override void _Ready() {
        LevelsContainer = new Node3D[ GetNode<Node>("/root/Main/LevelContainer").GetChildCount()];
        for (int i = 0; i < LevelsContainer.Length; i++)
            LevelsContainer[i] = (Node3D)GetNode<Node>("/root/Main/LevelContainer").GetChild(i);

        DataControl.LoadLevels(LevelsContainer);
        LoadLastLevel();
    }

    private void LoadLastLevel()
    {
        foreach (Node3D level in LevelsContainer)
        {
            if ((bool)level.GetMeta("IsComplete") == false)
                InitCurrentLevel(level);  
        }
        GetNode<Player>("/root/Main/Player").Show();
    }    

    private void InitCurrentLevel(Node3D currentLevel) {
        LevelObjectsNode = GetNode<Node3D>(currentLevel.GetPath() + "/LevelObjects");
        CurrentLevel = new(
            (string)currentLevel.GetMeta("Name"),
            (int)currentLevel.GetMeta("Id"),
            (Vector3[])currentLevel.GetMeta("GridSize"),
            (int)currentLevel.GetMeta("Steps"),
            new Node3D[LevelObjectsNode.GetChildCount()]
        );

        for (int i = 0; i < CurrentLevel.Objects.Length; i++)
            CurrentLevel.Objects[i] = (Node3D)LevelObjectsNode.GetChild(i);
        CurrentSteps = CurrentLevel.Steps;

        currentLevel.Position = Vector3.Zero;
    }

    //NOTE!!!
    //Add SaveLevels() on finish player
    public void PlayerFinished(Node3D body) {
        GD.Print("You win!");
        GetNode<Player>("/root/Main/Player").LeaveFromLevel();
    }
}
