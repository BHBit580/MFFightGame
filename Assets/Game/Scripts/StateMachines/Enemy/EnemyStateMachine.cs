using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [field: Header("Dependencies")]
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public GameObject SpecialBall { get; private set; }
    [field: SerializeField] public CoolDownSystem CoolDownSystem { get; private set; }
    [field: SerializeField] public WeaponDamage[] WeaponDamage { get; private set; }
    
    [field: SerializeField] public VoidEventChannelSO QSpecialAnimationEvent;
    
    [field: Header("Special Attack Settings")]
    [field: SerializeField] public Vector3 SpecialAttackOffset { get; private set; }
    [field: SerializeField] public float SpecialAttackRange { get; private set; }
    [field: SerializeField] public float SpecialAttackBackwardSpeed { get; private set; }
    [field: SerializeField] public float SpecialAttackDamage { get; private set; } 
    
    [field: Header("Camera Shake Settings")]
    [field: SerializeField] public CinemachineShake CinemachineShake { get; private set; }
    [field: SerializeField] public float ShakeIntensity { get; private set; }
    [field: SerializeField] public float ShakeTime { get; private set; }
    
    [field: Header("Hit Vfx Settings")]
    [field: SerializeField] public GameObject Vfx { get; private set; }
    [field: SerializeField] public Vector3 VfxOffset { get; private set; }
    
    [field: Header("Attack Settings")]
    [field: SerializeField] public float MinDistanceToStartAttack { get; private set; }
    [field: SerializeField] public float AttackMaxRange { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    
    [field: Header("Basic Enemy Attacks")]
    [field: SerializeField] public List<EnemyAttack> BasicEnemyAttacks { get; private set; }

    

    
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public CharacterHealth CharacterHealth { get; private set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        CharacterHealth = GetComponent<CharacterHealth>();
        Animator = GetComponent<Animator>();
        SwitchState(new EnemyInitialState(this));
    }
}
