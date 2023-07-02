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
            movementTarget.targetPosition = RoundVector(Position + movementTarget.targetDirection, 1);
            playerStates = States.MOVEMENT;
        }
    }

    private void Move() {
        if (movementTarget.targetPosition.X >= CurrentLevel.GetLevelGridSize()[0].X && movementTarget.targetPosition.X <= CurrentLevel.GetLevelGridSize()[1].X &&
        movementTarget.targetPosition.Z >= CurrentLevel.GetLevelGridSize()[0].Z && movementTarget.targetPosition.Z <= CurrentLevel.GetLevelGridSize()[1].Z) {
            if (RoundVector(Position, 1) != movementTarget.targetPosition)
                LinearVelocity = movementTarget.targetDirection;
            else {
                LinearVelocity = Vector3.Zero;
                playerStates = States.NORMAL;                
            }
        }
        else {
            playerStates = States.NORMAL;
            GD.PushWarning("Out of a level!");
        }
    }

    //-------------------------------------------------------------------------------|
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
    //-------------------------------------------------------------------------------|
}