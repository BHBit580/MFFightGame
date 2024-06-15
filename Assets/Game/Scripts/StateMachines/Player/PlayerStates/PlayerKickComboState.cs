using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKickComboState : PlayerBaseState
{
    private ComboAttack _currentAttack;
    private int _currentIndex;
    private float _timer;
    
    public PlayerKickComboState(PlayerStateMachine stateMachine , int attackIndex) : base(stateMachine)
    {
        _currentIndex = attackIndex;
        _currentAttack = stateMachine.KickAttacks[_currentIndex];
    }

    public override void Enter()
    {
        _timer = 0f;
        foreach (WeaponDamage weaponDamage in stateMachine.WeaponDamage)
        {
            weaponDamage.SetShakeValues(stateMachine.ShakeIntensity , stateMachine.ShakeTime , stateMachine.CinemachineShake);
            weaponDamage.SetAttackValues(_currentAttack.Damage , _currentAttack.KnockBack);
            weaponDamage.SetVfx(stateMachine.VfxHitParticle , stateMachine.VfxHitOffset);
        }
        
        stateMachine.Animator.CrossFadeInFixedTime(_currentAttack.AnimationName , _currentAttack.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        _timer += deltaTime;
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime > _currentAttack.MinimumNormalizedTime && _timer < _currentAttack.ComboWindowTime)
        {
            if (stateMachine.InputReader.KickAttack)
            {
                TryComboAttack();
            }
        }

        if (normalizedTime >=1f)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    private void TryComboAttack()
    {
        if(_currentIndex == stateMachine.PunchAttacks.Count-1) return;                 //Last attack
        _currentIndex++;
        
        stateMachine.SwitchState
        (
            new PlayerKickComboState(stateMachine , _currentIndex)
        );
    }
    
    public override void Exit()
    {
        
    }
}