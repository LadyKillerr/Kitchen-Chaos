using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;

    [Header("Movement Section")]

    float currentSpeed;
    [SerializeField] float walkSpeed = 250f;
    [SerializeField] float sprintSpeed = 350f;
    Vector3 moveInput;
    [SerializeField] float rotateSpeed = 10f;

    Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
    }

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        playerInput = GetComponent<PlayerInput>();

        currentSpeed = walkSpeed;


    }

    private void FixedUpdate()
    {
        PlayerMovement();

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector3>();
    }

    void OnSprint(InputValue value)
    {
        bool sprintInput = value.isPressed;
        if (sprintInput)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
        UnityEngine.Debug.Log(sprintInput);
    }

    void PlayerMovement()
    {
        playerRigidbody.velocity = new Vector3(moveInput.x * currentSpeed * Time.deltaTime, 0, moveInput.z * currentSpeed * Time.deltaTime);

        Vector3 lookDirection = moveInput;
        
        transform.forward = Vector3.Slerp(transform.forward, lookDirection, Time.deltaTime * rotateSpeed);
    }


}
