using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] InputActionReference move, attack, sprint;

    //void OnSprint(InputValue value)
    //{
    //    player.SetIsRunning(value.isPressed);
    //}


    //void OnMove(InputValue value)
    //{
    //    moveInput = value.Get<Vector3>();
        
    //    // Look in the processors of the input actions settings
    //    moveInput = moveInput.normalized;
    //}

    public Vector3 GetMovementVectorNormalized()
    {
        Vector3 inputVector = move.action.ReadValue<Vector3>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public bool GetSprintState()
    {
        bool isSprint = sprint.action.IsPressed();

        return isSprint;
    }
}
                             