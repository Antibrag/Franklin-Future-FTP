using Godot;
using System;

enum States {
    NORMAL,
    MOVEMENT
}

struct Target {
    public Vector3 targetDirection;
    public Vector3 targetPosition;
}

public partial class Player : RigidBody3D {
    private States playerStates = States.NORMAL;
    private Target movementTarget;

    public override void _Process(double delta) {
        StatesControl(playerStates);
        GD.Print("targetDirection = " + movementTarget.targetDirection + "\nLinearVelocity = " + 
        LinearVelocity + "\nPlayer Position = " + Position + "\nPlayer state = " + playerStates + "\n");
    }

    private void StatesControl(States state) {
        switch (state) {
            case States.NORMAL:
                CheckPlayerInput();
                break;
            case States.MOVEMENT:
                Move();
                break;
        }
    }

    private void CheckPlayerInput() {
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        if (direction != Vector3.Zero) {
            movementTarget.targetDirection = direction;
            movementTarget.targetPosition = Position + movementTarget.targetDirection;
            playerStates = States.MOVEMENT;
        }
    }

    private void Move() {
        GD.Print("Target position = " + movementTarget.targetPosition);

        if (Position != movementTarget.targetPosition)
                LinearVelocity = movementTarget.targetDirection;
            else
                playerStates = States.NORMAL;

        // if (targetPosition.X > CurrentLevel.GetLevelGridSize()[0].X && targetPosition.X < CurrentLevel.GetLevelGridSize()[1].X &&
        // targetPosition.Z > CurrentLevel.GetLevelGridSize()[0].Z && targetPosition.Z < CurrentLevel.GetLevelGridSize()[1].Z)
        // {
            
        // }
        // else
        // {
        //     playerStates = States.NORMAL;
        //     GD.PushWarning("Out of a world!");
        // }

    }
}