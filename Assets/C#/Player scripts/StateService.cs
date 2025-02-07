using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static States;

public class StateService:IState
{
    private MovementState state;
    private Animator anim;
    
    public StateService(Player player)
    {
        anim = player.GetComponent<Animator>();
    }

    public void UpdateState(Player player)
    {
        if (player.isSliding )
        {
            state = MovementState.PlayerSlide;
            Debug.Log(state);
        }
        else if(player.rb.velocity.y > 0.1f)
        {
            state = MovementState.PLayerJump;
        }
        else if (player.rb.velocity.y < -.1f)
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