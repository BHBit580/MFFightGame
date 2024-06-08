using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private readonly int WalkingHash = Animator.StringToHash("Walking");
    private const float CrossFadeDuration = 0.1f;
    
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(WalkingHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (DistanceBetweenPlayerAndEnemy() < stateMachine.MinDistanceToStartAttack)
        {
            stateMachine.SwitchState(new EnemyCombactState(stateMachine));
        }
        
        stateMachine.transform.position = Vector3.MoveTowards(stateMachine.transform.position, stateMachine.Player.transform.position,
            stateMachine.Speed * deltaTime);
    }

    public override void Exit()
    {
       
    }
}
