using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateMachine : MonoBehaviour
{
    private Rigidbody2D playerRb2D;
    private Animator anim;

    private void Start()
    {
        
        playerRb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UndateMovementState();
    }

    private enum MovementState
    {
        Player,
        PlayerIdle,
        PLayerJump,
        PLayerFall,
        PlayerSlide,
        PlayerDeath
        
    }

    public void UndateMovementState()
    {
        MovementState state;

        if(playerRb2D.velocity.y > 0.1f)
        {
            state = MovementState.PLayerJump;
        }
        else if (playerRb2D.velocity.y < -.1f)
        {
            state = MovementState.PLayerFall;
        }

        else
        {
            state = MovementState.PlayerIdle;
        }







        anim.SetInteger("state", (int)state);
    }

   
}
