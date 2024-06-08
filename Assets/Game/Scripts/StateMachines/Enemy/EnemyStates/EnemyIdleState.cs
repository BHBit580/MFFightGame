using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int FightIdle = Animator.StringToHash("FightIdle");
    private const float CrossFadeDuration = 0.1f;
    
    
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.CharacterHealth.CharacterGotNormalHitEvent += GoToNormalHitState;
        stateMachine.QSpecialAnimationEvent.RegisterListener(GoToQSpecialHitState);
        stateMachine.Animator.CrossFadeInFixedTime(FightIdle , CrossFadeDuration);

        GoToChaseState();
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.transform.position =
            new Vector3(stateMachine.transform.position.x, stateMachine.transform.position.y, 0);
    }

    private void GoToChaseState()
    {
        if (stateMachine.isFirstTimeChase)
        {
            int randomTime = Random.Range(1, 3);
            DOVirtual.DelayedCall(randomTime, () => {
                stateMachine.SwitchState(new EnemyChaseState(stateMachine));
                stateMachine.isFirstTimeChase = false;
            });
        }
        else
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }
    }

    private void GoToNormalHitState()
    {
        stateMachine.SwitchState(new EnemyNHitReactionState(stateMachine));
    }
    
    private void GoToQSpecialHitState()
    {
        stateMachine.SwitchState(new EnemyQSpecialReactionState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.CharacterHealth.CharacterGotNormalHitEvent -= GoToNormalHitState;
        stateMachine.QSpecialAnimationEvent.UnregisterListener(GoToQSpecialHitState);
    }
}
