using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWinState : EnemyBaseState
{
    private readonly int WinAnimationHash = Animator.StringToHash("Win");
    private const float CrossFadeDuration = 0.4f;
    private readonly int NormalPoseAnimationHash = Animator.StringToHash("NormalPose");
    private bool normalPoseAnimationCrossFadeCalled = false;
    public EnemyWinState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(WinAnimationHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (CheckAnimationCompleted(stateMachine.Animator, WinAnimationHash) && !normalPoseAnimationCrossFadeCalled)
        {
            stateMachine.Animator.CrossFadeInFixedTime(NormalPoseAnimationHash , CrossFadeDuration);
            normalPoseAnimationCrossFadeCalled = true;
        }
    }

    public override void Exit()
    {
      
    }
}
