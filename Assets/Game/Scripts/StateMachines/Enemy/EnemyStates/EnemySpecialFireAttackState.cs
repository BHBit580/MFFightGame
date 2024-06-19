using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpecialFireAttackState : EnemyBaseState
{
    private readonly int SpecialThrowHash = Animator.StringToHash("SpecialThrow");
    
    private const float CrossFadeDuration = 0.1f;
    private bool hasInstantiatedObject;
    private bool hasDone;
    

    private GameObject fire;
    
    public EnemySpecialFireAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(SpecialThrowHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        InstantiateBallConditions();
    }

    private void InstantiateBallConditions()
    {
        if (!hasInstantiatedObject && CheckAnimationPercentCompleted(stateMachine.Animator, 0.2f, SpecialThrowHash))
        {
            fire = GameObject.Instantiate(stateMachine.SpecialBall, stateMachine.transform.position + stateMachine.SpecialAttackOffset, 
                stateMachine.SpecialBall.transform.rotation);
            hasInstantiatedObject = true;
        }
        

        if (CheckAnimationPercentCompleted(stateMachine.Animator, 0.65f, SpecialThrowHash))
        {
            GameObject.Destroy(fire);
        }

        if (CheckAnimationCompleted(stateMachine.Animator, SpecialThrowHash))
        {
            stateMachine.SwitchState(new EnemyFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        GameObject.Destroy(fire);
    }
    
    
}
