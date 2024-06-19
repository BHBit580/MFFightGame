using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombactState : EnemyBaseState 
{
    private readonly int FightIdle = Animator.StringToHash("FightIdle");
    
    private int CurrentAnimationHash;
    private const float CrossFadeDuration = 0.1f;
    private EnemyAttack currentAttack;
    
    public EnemyCombactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }
    
    public override void Enter()
    {
        PickRandomAttack();
        
        CurrentAnimationHash = Animator.StringToHash(stateMachine.BasicEnemyAttacks[0].AnimationName);            //First attack animation
        
        SetWeaponValues();
        
        stateMachine.Animator.CrossFadeInFixedTime(CurrentAnimationHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (CheckAnimationCompleted(stateMachine.Animator, CurrentAnimationHash))
        {
            PickRandomAttack();
            SetWeaponValues();
            stateMachine.Animator.Play(CurrentAnimationHash, 0, 0);
        }

        SwitchToOtherStates();
    }

    
    private void PickRandomAttack()
    {
        currentAttack = stateMachine.BasicEnemyAttacks[Random.Range(0, stateMachine.BasicEnemyAttacks.Count)];
        CurrentAnimationHash = Animator.StringToHash(currentAttack.AnimationName);
    }
   
    public override void Exit()
    {

    }
    
    
    private void SetWeaponValues()
    {
        foreach (WeaponDamage weaponDamage in stateMachine.WeaponDamage)
        {
            weaponDamage.SetVfx(stateMachine.Vfx , stateMachine.VfxOffset);
            weaponDamage.SetAttackValues( currentAttack.Damage, currentAttack.KnockBack);
            weaponDamage.SetShakeValues(stateMachine.ShakeIntensity, stateMachine.ShakeTime, stateMachine.CinemachineShake);
        }
    }

    private void SwitchToOtherStates()
    {
        if (DistanceBetweenPlayerAndEnemy() > stateMachine.AttackMaxRange)
        {
            stateMachine.SwitchState(new EnemyFreeLookState(stateMachine));
            
            
            if (Random.Range(0 , 3) == 2)
            {
                stateMachine.SwitchState(new EnemySpecialFireAttackState(stateMachine));
            }
        }
    }
    
}
