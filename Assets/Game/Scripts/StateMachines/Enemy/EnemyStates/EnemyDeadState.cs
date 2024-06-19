using System.Collections;
using System.Collections.Generic;
using DissolveExample;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private readonly int DeathHash = Animator.StringToHash("Death");
    private const float CrossFadeDuration = 0.4f;
    private bool hasDone;
    
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.EnemyDiedEvent.RaiseEvent();
        stateMachine.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        stateMachine.Animator.CrossFadeInFixedTime(DeathHash , CrossFadeDuration);
        stateMachine.GetComponent<WeaponActivationHandler>().DisableWeapon();
        stateMachine.GetComponent<ForceReceiver>().enabled = false;
        stateMachine.GetComponent<CapsuleCollider>().enabled = false;
    }

    public override void Tick(float deltaTime)
    {
        if (CheckAnimationCompleted(stateMachine.Animator, DeathHash) && !hasDone)
        {
            stateMachine.Animator.enabled = false;
            stateMachine.GetComponentInChildren<SkinnedMeshRenderer>().material = stateMachine.DeathMaterial;
            stateMachine.AddComponent<DissolveChilds>();
            hasDone = true;
        }

        if (hasDone && stateMachine.GetComponent<DissolveChilds>().CheckDissolveCompleted())
        {
            stateMachine.GetComponent<EnemyStateMachine>().enabled = false;
        }
    }

    public override void Exit()
    {
        
    }
}
