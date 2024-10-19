using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombactState : EnemyBaseState 
{
    private readonly int FightIdle = Animator.StringToHash("FightIdle");
    
    private int CurrentHASH;
    private const float CrossFadeDuration = 0.1f;
    private EnemyAttack currentAttack;
    
    public EnemyCombactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }
    
    public override void Enter()
    {
        PlayFirstAttackAnimation();
    }
    
    private void PlayFirstAttackAnimation()
    {
        currentAttack = stateMachine.BasicEnemyAttacks[0];
        CurrentHASH = Animator.StringToHash(currentAttack.AnimationName);            
        SetWeaponValues();
        stateMachine.Animator.CrossFadeInFixedTime(CurrentHASH, CrossFadeDuration);
    }
    
    public override void Tick(float deltaTime)
    {
        if (CheckAnimationCompleted(stateMachine.Animator, CurrentHASH))
        {
           AttackDecisionMaker();
        }

        SwitchToOtherStates();
    }

    private void AttackDecisionMaker()
    {
        int random = Random.Range(0, 4);
        if (random == 2)
        {
            stateMachine.SwitchState(new EnemySpecialFireAttackState(stateMachine));
        }
        else
        {
            DoRandomAttack();
        }
    }
    
    
    private void DoRandomAttack()
    {
        currentAttack = stateMachine.BasicEnemyAttacks[Random.Range(0, stateMachine.BasicEnemyAttacks.Count)];
        CurrentHASH = Animator.StringToHash(currentAttack.AnimationName);
        SetWeaponValues();
        stateMachine.Animator.CrossFadeInFixedTime(currentAttack.AnimationName, CrossFadeDuration);
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
