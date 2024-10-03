using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;

    [SerializeField] Player player;
    [SerializeField] InputActionReference move, sprint, interact;

    void Awake()
    {
        // interact input listener
        interact.action.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        //if (OnInteractAction != null) { OnInteractAction(this, EventArgs.Empty); }
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

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
                             