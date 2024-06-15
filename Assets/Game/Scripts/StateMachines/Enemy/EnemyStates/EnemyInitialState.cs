using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyInitialState : EnemyBaseState
{
    private readonly int FightIdle = Animator.StringToHash("FightIdle");


    private const float CrossFadeDuration = 0.1f;
    
    public EnemyInitialState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FightIdle , CrossFadeDuration);
        int randomTime = Random.Range(1, 3);
        DOVirtual.DelayedCall(randomTime, () => {
            stateMachine.SwitchState(new EnemyFreeLookState(stateMachine));
        });
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}


