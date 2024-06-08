using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void ConstrainPlayerPosition()
    {
        stateMachine.transform.position = new Vector3(stateMachine.transform.position.x , stateMachine.transform.position.y , 0);
    }
    
}