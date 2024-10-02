using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(GameInput))]
public class Player : MonoBehaviour
{
    [SerializeField] GameInput gameInput;

    [Header("Player Movement")]
    Vector3 lastInteractDirection;
    [SerializeField] float currentSpeed;
    [SerializeField] float walkSpeed = 250f;
    [SerializeField] float sprintSpeed = 350f;
    [SerializeField] float rotateSpeed = 10f;

    [Header("Player Interactions")]
    [SerializeField] float interactDistance = 2f;
    [SerializeField] LayerMask countersLayerMask;

    [Header("Player Animations")]
    PlayerAnimation playerAnimation;

    Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    void Start()
    {
        Setup();
    }

    void Setup()
    {

        currentSpeed = walkSpeed;


    }

    // Fixed Update are for movement 
    private void FixedUpdate()
    {
        PlayerMovement();



    }

    void Update()
    {
        HandleInteractions();
    }

    #region PlayerInteractions

    void HandleInteractions()
    {
        // if not save look direction somewhere then when the player stops, the raycast is not pointing anywhere.
        Vector3 lookDirection = gameInput.GetMovementVectorNormalized();

        if (lookDirection != Vector3.zero)
        {
            // trying to keep track of the lookDirection even when not moving
            lastInteractDirection = lookDirection;
        }

        // this raycastHitInfo will let us know what object hit the raycast collision
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHitInfo, interactDistance, countersLayerMask))
        // added in countersLayerMask to only point the raycast towards objects marked with counters layers, others object will not be called even tho the raycast is pointing towards it 
        {
            if (raycastHitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.IsInteracted();
            }
        }
    }

    #endregion

    #region PlayerMovement

    void PlayerMovement()
    {
        Vector3 movementDirection = gameInput.GetMovementVectorNormalized();
        playerRigidbody.velocity = movementDirection * Time.deltaTime * currentSpeed;

        // manipulate running speed and movement animation
        if (gameInput.GetSprintState() && playerRigidbody.velocity != Vector3.zero)
        {
            currentSpeed = sprintSpeed;
            playerAnimation.RunningAnimation(true);
        }
        else if (!gameInput.GetSprintState() && playerRigidbody.velocity != Vector3.zero)
        {
            currentSpeed = walkSpeed;
            playerAnimation.RunningAnimation(false);
        }
        else if (playerRigidbody.velocity == Vector3.zero)
        {
            playerAnimation.IdlingAnimation();
        }

        Vector3 lookDirection = gameInput.GetMovementVectorNormalized();

        // make player face turns slowly toward his moving direction
        transform.forward = Vector3.Slerp(transform.forward, lookDirection, Time.deltaTime * rotateSpeed);
    }

    #endregion

    #region PlayerSetter


    #endregion

}
