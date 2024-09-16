using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameInput gameInput;

    [Header("Movement Section")]
    float currentSpeed;

    bool isRunning;
    public bool IsRunning { get { return isRunning; } }

    bool isWalking;
    public bool IsWalking { get { return isWalking; } }

    [SerializeField] float walkSpeed = 250f;
    [SerializeField] float sprintSpeed = 350f;
    [SerializeField] float rotateSpeed = 10f;

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

    // Update are for normal functions
    private void Update()
    {
        
    }

    void PlayerMovement()
    {
        playerRigidbody.velocity = new Vector3(gameInput.MoveInput.x * currentSpeed * Time.deltaTime, 0, gameInput.MoveInput.z * currentSpeed * Time.deltaTime);


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

        //UnityEngine.Debug.Log(playerRigidbody.velocity);

        Vector3 lookDirection = gameInput.MoveInput ;
        
        // make player face turns slowly toward his moving direction
        transform.forward = Vector3.Slerp(transform.forward, lookDirection, Time.deltaTime * rotateSpeed);

        // Check what is infront of the player
        float rayLength = 0.7f;
        Physics.Raycast(transform.position, lookDirection, rayLength);
    
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
