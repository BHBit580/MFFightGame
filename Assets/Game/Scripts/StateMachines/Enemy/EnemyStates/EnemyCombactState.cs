using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombactState : EnemyBaseState
{
    private int CurrentAnimationHash;
    private const float CrossFadeDuration = 0.1f;
    private EnemyAttack currentAttack;
    
    public EnemyCombactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }
    
    public override void Enter()
    {
        PickRandomAttack();
        
        CurrentAnimationHash = Animator.StringToHash(stateMachine.BasicEnemyAttacks[0].AnimationName);
        stateMachine.CharacterHealth.CharacterGotNormalHitEvent += SwitchToHitReactionState;
        
        foreach (WeaponDamage weaponDamage in stateMachine.WeaponDamage)
        {
            weaponDamage.SetVfx(stateMachine.Vfx , stateMachine.VfxOffset);
            weaponDamage.SetAttackValues( currentAttack.Damage, currentAttack.KnockBack);
            weaponDamage.SetShakeValues(stateMachine.ShakeIntensity, stateMachine.ShakeTime, stateMachine.CinemachineShake);
        }
        
        stateMachine.Animator.CrossFadeInFixedTime(CurrentAnimationHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (CheckAnimationCompleted(stateMachine.Animator, CurrentAnimationHash))
        {
            PickRandomAttack();
            stateMachine.Animator.Play(CurrentAnimationHash, 0, 0);
        }

        if (DistanceBetweenPlayerAndEnemy() > stateMachine.AttackMaxRange)
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }
    }
    
    private void PickRandomAttack()
    {
        currentAttack = stateMachine.BasicEnemyAttacks[Random.Range(0, stateMachine.BasicEnemyAttacks.Count)];
        CurrentAnimationHash = Animator.StringToHash(currentAttack.AnimationName);
    }
    
    private void SwitchToHitReactionState()
    {
        stateMachine.SwitchState(new EnemyNHitReactionState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.CharacterHealth.CharacterGotNormalHitEvent -= SwitchToHitReactionState;
    }
}
