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
    [Export]
    public PackedScene[] Levels;
    [Export]
    public PackedScene MainScene;

    public Node3D LevelObjectsNode;
    public static Level CurrentLevel;
    public static int CurrentSteps;
    private Node3D instanceLevel;

	public override void _Ready() {
        LoadLastLevel();
    }

    public void LoadLastLevel() {
        foreach (PackedScene level in Levels) {
            var localScene = level.Instantiate();
            if ((bool)localScene.GetMeta("IsComplete") == false) {
                instanceLevel = (Node3D)localScene;
                GetNode<Node>("/root/Main").AddChild(instanceLevel);

                
                return;
            }
            else 
                localScene.Free();
        }
        GD.PrintErr("All scene has been completed!");
    }    

    private void InitCurrentLevel() {
        CurrentLevel = new(
            (string)GetMeta("Name"),
            (int)GetMeta("Id"),
            (Vector3[])GetMeta("GridSize"),
            (int)GetMeta("Steps"),
            new Node3D[LevelObjectsNode.GetChildCount()]
        );

        for (int i = 0; i < CurrentLevel.Objects.Length; i++)
            CurrentLevel.Objects[i] = (Node3D)LevelObjectsNode.GetChild(i);
    }

    //NOTE!!!
    //Add SaveLevels() on finish player
    public void PlayerFinished(Node3D body) {
        GD.Print("You win!");
        GetNode<Player>("/root/Main/Player").LeaveFromLevel();
    }
}
