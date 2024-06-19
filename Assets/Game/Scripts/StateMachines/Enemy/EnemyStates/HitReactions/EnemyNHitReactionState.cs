using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNHitReactionState : EnemyBaseState , IHasCoolDown
{
    private int HitReactionHash = Animator.StringToHash("Hit1");
    private const float CrossFadeDuration = 0.1f;
    private int Id = 1012;
    private float coolDownTime = 0.5f;

    
    public EnemyNHitReactionState(EnemyStateMachine stateMachine) : base(stateMachine)
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
            stateMachine.SwitchState(new EnemyFreeLookState(stateMachine));
        }
    }

    public override void Tick(float deltaTime)
    {
        if(CheckAnimationPercentCompleted(stateMachine.Animator , 0.6f  , HitReactionHash))
        {
            stateMachine.SwitchState(new EnemyFreeLookState(stateMachine));
        }
    }
    
    public override void Exit()
    {
        
    }

    public int ID => Id;
    public float CoolDownDuration => coolDownTime;
}
