using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitReaction : PlayerBaseState , IHasCoolDown
{
    private int HitReactionHash = Animator.StringToHash("Hit");
    private const float CrossFadeDuration = 0.1f;
    private int Id = 4018;
    private float coolDownTime = 0.2f;

    public PlayerHitReaction(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        bool isOnCooldown = stateMachine.CoolDownSystem.IsInCoolDown(Id);
    
        if (!isOnCooldown)
        {
            stateMachine.Animator.CrossFadeInFixedTime(HitReactionHash, CrossFadeDuration);
            stateMachine.CoolDownSystem.StartCoolDown(this);
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
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

    public int ID => Id;
    public float CoolDownDuration => coolDownTime;
}
