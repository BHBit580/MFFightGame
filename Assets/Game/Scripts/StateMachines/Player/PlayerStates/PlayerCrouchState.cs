using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    private readonly int CrouchHash = Animator.StringToHash("Crouch");
    private const float CrossFadeDuration = 0.1f;

    private float initialHeight, initialCentreY;
    private float crouchHeight = 1, crouchCentreY = 0.65f;
    
    CapsuleCollider playerCollider;
    public PlayerCrouchState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        playerCollider = stateMachine.GetComponent<CapsuleCollider>();
        initialHeight = playerCollider.height;
        initialCentreY = playerCollider.center.y;
        stateMachine.Animator.CrossFadeInFixedTime(CrouchHash , CrossFadeDuration);
        SetCapsuleValues(crouchHeight , crouchCentreY);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsCrouching == false)
        {
            SetCapsuleValues(initialHeight , initialCentreY);
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
    
    private void SetCapsuleValues(float height , float centreY)
    {
        playerCollider.height = height;
        playerCollider.center = new Vector3(playerCollider.center.x, centreY, playerCollider.center.z);
    }
    
}
