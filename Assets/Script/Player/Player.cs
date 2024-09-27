using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    [SerializeField] GameInput gameInput;

    [Header("Movement Section")]
    float currentSpeed;

    bool isRunning;
    public bool IsRunning { get { return isRunning; } }

    bool isWalking;
    public bool IsWalking { get { return isWalking; } }

    [Header("Player Movement")]
    [SerializeField] float walkSpeed = 250f;
    [SerializeField] float sprintSpeed = 350f;
    [SerializeField] float rotateSpeed = 10f;

    [Header("PlayerInteractions")]
    [SerializeField] float interactDistance = 2f;
    [SerializeField] LayerMask countersLayerMask;

    Vector3 lastInteractDirection;



    Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
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

    void HandleInteractions()
    {
        // if not save look direction somewhere then when the player stops, the raycast is not pointing anywhere.
        Vector3 lookDirection = gameInput.MoveInputNormalized;

        if(lookDirection != Vector3.zero)
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

    void PlayerMovement()
    {
        playerRigidbody.velocity = new Vector3(gameInput.MoveInputNormalized.x * currentSpeed * Time.deltaTime, 0, gameInput.MoveInputNormalized.z * currentSpeed * Time.deltaTime);

        // manipulate running speed
        if (isRunning)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        // isWalking is true when player velocity != 0
        if (playerRigidbody.velocity != Vector3.zero && !isRunning)
        {
            isWalking = true;
        }
        else if (playerRigidbody.velocity != Vector3.zero && isRunning)
        {
            isRunning = true;
        }
        else if (playerRigidbody.velocity == Vector3.zero)
        {
            isWalking = false;
            isRunning = false;
        }

        Vector3 lookDirection = gameInput.MoveInputNormalized;

        // make player face turns slowly toward his moving direction
        transform.forward = Vector3.Slerp(transform.forward, lookDirection, Time.deltaTime * rotateSpeed);

        // Check what is infront of the player
        //float moveDistance = currentSpeed * Time.deltaTime;
        //float playerRadius = 0.5f;
        //float playerHeight = 2f;

        //bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, lookDirection, moveDistance);

        


    }
    #region PlayerSetter
    public void SetIsRunning(bool value)
    {
        isRunning = value;
    }

    public void SetIsWalking(bool value)
    {
        isWalking = value;
    }

    #endregion

}
