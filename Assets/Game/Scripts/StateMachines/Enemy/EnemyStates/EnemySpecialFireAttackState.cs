using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialFireAttackState : EnemyBaseState
{
    private readonly int SpecialThrowHash = Animator.StringToHash("SpecialThrow");
    private readonly int BackwardMovementHash = Animator.StringToHash("BackwardMovement");
    private const float CrossFadeDuration = 0.1f;
    private bool hasInstantiatedObject;
    private bool startFire;
    private GameObject fire;
    
    public EnemySpecialFireAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if (DistanceBetweenPlayerAndEnemy() <= stateMachine.SpecialAttackRange)
        {
            stateMachine.Animator.CrossFadeInFixedTime(SpecialThrowHash , CrossFadeDuration);
            startFire = true;
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(BackwardMovementHash , CrossFadeDuration);
        }
    }

    public override void Tick(float deltaTime)
    {
        if (startFire == false)
        {
            stateMachine.transform.Translate(Vector3.forward *(stateMachine.SpecialAttackBackwardSpeed * deltaTime));
            
            if(DistanceBetweenPlayerAndEnemy() <= stateMachine.SpecialAttackRange)
            {
                stateMachine.Animator.CrossFadeInFixedTime(SpecialThrowHash , CrossFadeDuration);
                startFire = true;
            }
        }
        
        InstantiateBallConditions();
    }

    private void InstantiateBallConditions()
    {
        if(!startFire) return;
        
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
        
    }
}
