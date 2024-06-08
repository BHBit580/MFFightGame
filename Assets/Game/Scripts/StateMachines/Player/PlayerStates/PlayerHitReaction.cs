using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitReaction : PlayerBaseState
{
    private int HitReactionHash = Animator.StringToHash("Hit");
    private const float CrossFadeDuration = 0.1f;

    public PlayerHitReaction(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(HitReactionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (CheckAnimationPercentCompleted(stateMachine.Animator, 0.6f, HitReactionHash))
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
