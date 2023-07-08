using Godot;
using System;

//Player states
enum States {
    NORMAL,     //Player can pressed movement buttons 
    MOVEMENT,   //player can't pressed movement buttons, but player mesh moved
    DEATH       //Player can't pressed movement buttons, player mesy replaced to player spawn
}

//Structure target, has target properties
struct Target {
    public Vector3 targetDirection;     //Target direction - direction where player moved
    public Vector3 targetPosition;      //Target position - position where player moved
}

public partial class Player : RigidBody3D {
    private States playerState = States.NORMAL;
    private Target movementTarget;

    public override void _Ready() {
        Position = GetNode<Node3D>("/root/Main/Level-" + CurrentLevel.GetId() + "/Spawn").Position;
    }

    public override void _Process(double delta) {
        StatesControl(playerState);    //State system
    }

    //Realization state system
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
        if (LevelControl.CurrentSteps == 0) 
            playerState = States.DEATH;

        //Get input direction from Input action
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        if (direction != Vector3.Zero) {
            if (!(direction.X != 0 && direction.Z != 0)) {
                movementTarget.targetDirection = direction;
                movementTarget.targetPosition = RoundVector(Position + movementTarget.targetDirection, 1);  //Round target position to 10
                playerState = States.MOVEMENT;
            }            
        }
    }

    private void Move() {
        //Limited target position, because player don't moved outside level
        // !! Can be optimization. Relocate if, to CheckPlayerInput(), which the player did not change the state in void !!
        if (movementTarget.targetPosition.X >= CurrentLevel.GetGridSize()[0].X && movementTarget.targetPosition.X <= CurrentLevel.GetGridSize()[1].X &&
        movementTarget.targetPosition.Z >= CurrentLevel.GetGridSize()[0].Z && movementTarget.targetPosition.Z <= CurrentLevel.GetGridSize()[1].Z)
        {
            if (RoundVector(Position, 1) != movementTarget.targetPosition)
                LinearVelocity = movementTarget.targetDirection;
            else {
                LinearVelocity = Vector3.Zero;
                LevelControl.CurrentSteps--;
                GD.Print(LevelControl.CurrentSteps);
                playerState = States.NORMAL;                
            }
        }
        else {
            playerState = States.NORMAL;
            GD.PushWarning("Out of a level!");
        }
    }

    private void Death() {
        GD.Print("Death");
        Position = GetNode<Node3D>("/root/Main/Level-" + CurrentLevel.GetId() + "/Spawn").Position;
        LevelControl.CurrentSteps = CurrentLevel.GetSteps();
        playerState = States.NORMAL;
    }

    //I did realezation round vector, because I don't find working function
    //Vector3.Ceil() - dont fits, because he rounded vector to whole
    //Me need rounded Vector3 to 10
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