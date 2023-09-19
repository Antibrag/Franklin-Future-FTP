using Godot;
using System;

public partial class Player : RigidBody3D {
    private enum States {
        NORMAL,    
        MOVEMENT,   
        DEATH,       
        OUT_WORLD
    }

    private struct Target {
        public Vector3 targetDirection;
        public Vector3 targetPosition;
    }

    [Export]
    public PackedScene checkScene;

    private States playerState;
    private Target movementTarget;

    public override void _Ready() {
        playerState = States.NORMAL;
    }

    public override void _Process(double delta) {
        StatesControl(playerState);
    }

    public void UnFreezePlayer() {
        Show();
        Position = GetNode<Node3D>("/root/Main/LevelContainer/Level-" + LevelControl.CurrentLevel.Id + "/LevelObjects/Spawn").Position;
        playerState = States.NORMAL;
        GD.Print(Position);
    }

    private void StatesControl(States state) {
        switch (state) {
            case States.NORMAL:
                CheckPlayerInput();
            break;

            case States.MOVEMENT:
                Move();
            break;

            case States.DEATH:
                Death();
            break;
        }
    }

    private void CheckPlayerInput() {
        if (!GetNode<Timer>("MoveTimer").IsStopped())
            return;

        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        if (direction != Vector3.Zero && !(direction.X != 0 && direction.Z != 0))
        {
            movementTarget.targetDirection = direction;
            movementTarget.targetPosition = RoundVector(Position + movementTarget.targetDirection, 1);

            WallChecker wallChecker = GetNode<WallChecker>("/root/Main/WallChecker");
            wallChecker.Position = new Vector3(movementTarget.targetPosition.X, 0, movementTarget.targetPosition.Z);

            GetNode<Timer>("MoveTimer").Start();
        } 
    }

    private void Move() {
        if (RoundVector(Position, 1) != movementTarget.targetPosition)
            LinearVelocity = movementTarget.targetDirection;
        else
        {
            LinearVelocity = Vector3.Zero;
            GD.Print(LevelControl.CurrentLevel.Steps["CurrentSteps"]);
            playerState = States.NORMAL;                
        }                
    }

    private void Death() {
        GD.Print("Death");
        Position = GetNode<Node3D>("/root/Main/LevelContainer/Level-" + LevelControl.CurrentLevel.Id + "/Spawn").Position;
        LevelControl.CurrentLevel.Steps["CurrentSteps"] = LevelControl.CurrentLevel.Steps["C_LevelSteps"];
        playerState = States.NORMAL;
    }

    public void LeaveFromLevel() {
        LinearVelocity = Vector3.Zero;
        playerState = States.OUT_WORLD;
    }

	public void MoveTimer_Timeout() 
    {
        WallChecker wallChecker = GetNode<WallChecker>("/root/Main/WallChecker");

        if (!wallChecker.IsCollidingObj) 
        {
            LevelControl.CurrentLevel.Steps["CurrentSteps"]--;

            if (LevelControl.CurrentLevel.Steps["CurrentSteps"] < 0) 
            {
                playerState = States.DEATH;
                return;
            } 

            playerState = States.MOVEMENT;
        }        
    } 

    public void EnterInLevel() => playerState = States.NORMAL;

    private static Vector3 RoundVector(Vector3 vector, int digits) {
        Vector3 result = Vector3.Zero;
        for (int i = 0; i < 3; i++) {
            switch (i) {
                case 0:
                    result.X = MathF.Round(vector.X, digits);
                    break;
                case 1:
                    result.Y = MathF.Round(vector.Y, digits);
                    break;
                case 2:
                    result.Z = MathF.Round(vector.Z, digits);
                    break;
            }
        }
        return result;
    }
}