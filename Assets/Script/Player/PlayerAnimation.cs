using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_RUNNING = "IsRunning";

    [SerializeField] Player player;
    Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAnimationToggle();
    }

    private void MovementAnimationToggle()
    {
        playerAnimator.SetBool(IS_WALKING, player.IsWalking);
        playerAnimator.SetBool(IS_RUNNING, player.IsRunning);
    }



}
