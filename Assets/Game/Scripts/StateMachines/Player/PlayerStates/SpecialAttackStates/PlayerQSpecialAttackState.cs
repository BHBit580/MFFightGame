using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerQSpecialAttackState : PlayerBaseState
{
    private readonly int QSpecialAttackHash = Animator.StringToHash("QSpecialAttack");
    private const float CrossFadeDuration = 0.1f;
    private const float gameTimeScale = 0.065f;
    private bool hasInstantiatedObject;
    private ColorAdjustments colorAdjustments;
    
    public PlayerQSpecialAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(QSpecialAttackHash , CrossFadeDuration);
        Time.timeScale = gameTimeScale;
        if (stateMachine.Volume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            this.colorAdjustments = colorAdjustments;
            this.colorAdjustments.saturation.value = -100;
        }
    }

    public override void Tick(float deltaTime)
    {
        if (!hasInstantiatedObject && CheckAnimationPercentCompleted(stateMachine.Animator , 0.15f , QSpecialAttackHash))
        {
            Vector3 ballPosition = stateMachine.transform.position + stateMachine.QSpecialOffset;
            GameObject.Instantiate(stateMachine.QSpecialBall,ballPosition, Quaternion.identity);
            hasInstantiatedObject = true;
            Time.timeScale = 1f;
            colorAdjustments.saturation.value = 0f;
        }
        
        if (CheckAnimationCompleted(stateMachine.Animator , QSpecialAttackHash))
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

    }

    public override void Exit()
    {
        Time.timeScale = 1f;
        colorAdjustments.saturation.value = 0f;
    }
}
