using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFreeLookState : EnemyBaseState
{
    private readonly int WalkingHash = Animator.StringToHash("Walking");

    private const float CrossFadeDuration = 0.1f;
    private Tween fireAttackTween;
    
    public EnemyFreeLookState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        stateMachine.Animator.CrossFadeInFixedTime(WalkingHash , CrossFadeDuration);
        stateMachine.QSpecialAnimationEvent.RegisterListener(GoToQSpecialHitState);
        
        fireAttackTween = DOVirtual.DelayedCall(1f, RandomlyDoFireAttack).SetLoops(-1, LoopType.Restart);
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
    
    private void GoToQSpecialHitState()
    {
        stateMachine.SwitchState(new EnemyQSpecialReactionState(stateMachine));
    }

    private void RandomlyDoFireAttack()
    {
        int random = Random.Range(1, 3);
        if (random == 2)
        {
            stateMachine.SwitchState(new EnemySpecialFireAttackState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.QSpecialAnimationEvent.UnregisterListener(GoToQSpecialHitState);
        if (fireAttackTween != null)
        {
            fireAttackTween.Kill();
            fireAttackTween = null;
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (fireAttackTween != null)
        {
            fireAttackTween.Kill();
            fireAttackTween = null;
        }
    }
}
