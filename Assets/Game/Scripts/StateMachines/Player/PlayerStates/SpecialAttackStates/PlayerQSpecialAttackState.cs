using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerQSpecialAttackState : PlayerBaseState
{
    private readonly int QSpecialAttackHash = Animator.StringToHash("QSpecialAttack");
    private const float CrossFadeDuration = 0.1f;
    private bool hasInstantiatedObject;
    public PlayerQSpecialAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(QSpecialAttackHash , CrossFadeDuration);
        stateMachine.Animator.speed = 0.1f; 
    }

    public override void Tick(float deltaTime)
    {
        if (!hasInstantiatedObject && CheckAnimationPercentCompleted(stateMachine.Animator , 0.15f , QSpecialAttackHash))
        {
            Vector3 ballPosition = stateMachine.transform.position + stateMachine.QSpecialOffset;
            GameObject.Instantiate(stateMachine.QSpecialBall,ballPosition, Quaternion.identity);
            hasInstantiatedObject = true;
            stateMachine.Animator.speed = 1f;
        }
        
        if (CheckAnimationCompleted(stateMachine.Animator , QSpecialAttackHash))
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

    }

    public override void Exit()
    {
        stateMachine.Animator.speed = 1f;
    }
}
