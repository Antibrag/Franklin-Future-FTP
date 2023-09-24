using Godot;
using System;

public partial class Player : RigidBody3D 
{
    private enum States 
    {
        NORMAL,    
        MOVEMENT,   
        DEATH,       
        OUT_WORLD
    }

    private struct Target 
    {
        public Vector3 targetDirection;
        public Vector3 targetPosition;
    }

    [Export]
    public PackedScene checkScene;

    private States playerState;
    private Target movementTarget;

    public override void _Ready() => playerState = States.NORMAL;

    public override void _Process(double delta) 
    {
        StatesControl(playerState);
    }

    private void StatesControl(States state) 
    {
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

    private void CheckPlayerInput() 
    {
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

    private void Move()
    {
        if (RoundVector(Position, 1) != movementTarget.targetPosition)
            LinearVelocity = movementTarget.targetDirection;
        else
        {
            LinearVelocity = Vector3.Zero;

            if (RoundVector(Position, 1) == GetNode<Area3D>("/root/Main/LevelContainer/Level-" + LevelControl.CurrentLevel.Id + "/Finish").Position)
                Finish();

            playerState = States.NORMAL;                
        }                
    }

    private void Death() 
    {
        GD.Print("Death");
        Position = GetNode<Node3D>("/root/Main/LevelContainer/Level-" + LevelControl.CurrentLevel.Id + "/Spawn").Position;
        LevelControl.CurrentLevel.Steps["CurrentSteps"] = LevelControl.CurrentLevel.Steps["C_LevelSteps"];
        playerState = States.NORMAL;
    }

    private void Finish()
    {
        playerState = States.OUT_WORLD;
        LevelControl.CurrentLevel.Steps["CurrentSteps"] = 0;
        GetNode<HUD>("/root/Main/HUD").ShowFinishRect();

        GD.Print("Finished");
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

    private static Vector3 RoundVector(Vector3 vector, int digits) 
    {
        return new Vector3
        (
            MathF.Round(vector.X, digits),
            MathF.Round(vector.Y, digits),
            MathF.Round(vector.Z, digits)
        );
    }
}