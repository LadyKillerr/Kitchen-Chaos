using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    const string IS_WALKING = "IsWalking";
    const string IS_RUNNING = "IsRunning";

    Animator playerAnimator;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void RunningAnimation(bool val)
    {
        if (val)
        {
            playerAnimator.SetBool(IS_RUNNING, true);
            playerAnimator.SetBool(IS_WALKING, false);
        }
        else
        {
            playerAnimator.SetBool(IS_RUNNING, false);
            playerAnimator.SetBool(IS_WALKING, true);
        }
    }

    public void IdlingAnimation()
    {
        playerAnimator.SetBool(IS_RUNNING, false);
        playerAnimator.SetBool(IS_WALKING, false);
    }
}
