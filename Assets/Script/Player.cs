using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Section")]
    [SerializeField] float moveSpeed = 5f;


    Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
    }

    void Start()
    {

    }

    private void Update()
    {
        ControlMovement();
    }

    private void ControlMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);


        if (Input.GetKey(KeyCode.W))
        {
            if (playerRigidbody != null)
            {
                playerRigidbody.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.Force);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (playerRigidbody != null)
            {
                playerRigidbody.AddRelativeForce(Vector3.back * moveSpeed, ForceMode.Force);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (playerRigidbody != null)
            {
                playerRigidbody.AddRelativeForce(Vector3.left * moveSpeed, ForceMode.Force);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (playerRigidbody != null)
            {
                playerRigidbody.AddRelativeForce(Vector3.right * moveSpeed, ForceMode.Force);
            }
        }

        inputVector = inputVector.normalized;

        Debug.Log(inputVector);

    }
}
