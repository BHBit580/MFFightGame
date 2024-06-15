using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("Block");
    private const float CrossFadeDuration = 0.15f;
    
    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.GetComponent<CharacterHealth>().SetInvulnerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsBlocking == false)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.GetComponent<CharacterHealth>().SetInvulnerable(false);
    }
}
