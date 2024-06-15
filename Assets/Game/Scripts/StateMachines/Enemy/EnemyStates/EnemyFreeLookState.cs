using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyFreeLookState : EnemyBaseState
{
    private readonly int WalkingHash = Animator.StringToHash("Walking");

    private const float CrossFadeDuration = 0.1f;
    
    public EnemyFreeLookState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(WalkingHash , CrossFadeDuration);
        stateMachine.CharacterHealth.CharacterGotNormalHitEvent += GoToNormalHitState;
        stateMachine.QSpecialAnimationEvent.RegisterListener(GoToQSpecialHitState);
    }

    public override void Tick(float deltaTime)
    {
        RestrictPosition();
        stateMachine.transform.Translate(- Vector3.forward * (stateMachine.Speed * deltaTime));
        
        if (DistanceBetweenPlayerAndEnemy() < stateMachine.MinDistanceToStartAttack)
        {
            stateMachine.SwitchState(new EnemyCombactState(stateMachine));
        }
    }
    
    private void RestrictPosition()
    {
        stateMachine.transform.position = new Vector3(stateMachine.transform.position.x, stateMachine.transform.position.y, 0);
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
