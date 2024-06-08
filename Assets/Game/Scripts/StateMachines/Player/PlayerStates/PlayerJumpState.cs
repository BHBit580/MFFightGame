using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private const float CrossFadeDuration = 0.1f;
    
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash , CrossFadeDuration);
        stateMachine.Rigidbody.AddForce(Vector3.up  * stateMachine.JumpForce, ForceMode.Impulse);
    }

    public override void Tick(float deltaTime)
    {
        if (CheckAnimationCompleted(stateMachine.Animator, JumpHash))
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
