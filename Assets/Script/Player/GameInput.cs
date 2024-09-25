using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    [SerializeField] Player player;

    Vector3 moveInput;
    public Vector3 MoveInput{get { return moveInput; }}


    void OnSprint(InputValue value)
    {
        player.SetIsRunning(value.isPressed);
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector3>();
        
        // Look in the processors of the input actions settings
        moveInput = moveInput.normalized;
    }


}
