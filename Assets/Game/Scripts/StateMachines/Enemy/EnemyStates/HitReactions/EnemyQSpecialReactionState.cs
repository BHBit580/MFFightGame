using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQSpecialReactionState : EnemyBaseState
{
    private int HitReactionQSpecialHash = Animator.StringToHash("HitReactionQSpecial");
    private int StandUpHash = Animator.StringToHash("StandUp");

    private readonly float CrossFadeDuration = 0.1f;
    private bool hasDone = false;

    public EnemyQSpecialReactionState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(HitReactionQSpecialHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        
        if (currentInfo.shortNameHash == HitReactionQSpecialHash && currentInfo.normalizedTime >=1f && !hasDone)
        {
            stateMachine.Animator.CrossFadeInFixedTime(StandUpHash , CrossFadeDuration);
            hasDone = true;
        }
        
        if(currentInfo.shortNameHash == StandUpHash && currentInfo.normalizedTime >= 1f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
