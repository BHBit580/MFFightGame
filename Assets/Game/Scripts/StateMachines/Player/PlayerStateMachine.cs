using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(WeaponActivationHandler))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class PlayerStateMachine : StateMachine
{
    [field: Header("{Dependencies}")]
    [field: SerializeField] public WeaponDamage[] WeaponDamage { get; private set; }
    [field: SerializeField] public Feet Feet { get; private set; }
    [field: SerializeField] public VoidEventChannelSO PlayerDiedEvent { get; private set; }
    [field: SerializeField] public CoolDownSystem CoolDownSystem { get; private set; }
    [field: SerializeField] public Material DeathMaterial { get; private set; }
    [field: SerializeField] public Volume Volume { get; private set; }
    
    [field: Header("{Camera Shake Settings}")]
    [field: SerializeField] public CinemachineShake CinemachineShake { get; private set; }
    [field: SerializeField] public float ShakeIntensity { get; private set; }
    [field: SerializeField] public float ShakeTime { get; private set; }
    
    [field: Header("{Vfx hit effect}")]
    [field: SerializeField] public GameObject VfxHitParticle { get; private set; }
    [field: SerializeField] public Vector3 VfxHitOffset { get; private set; }
    
    [field: Header("{Special Attack Settings}")]
    [field: SerializeField] public GameObject QSpecialBall { get; private set; }
    [field: SerializeField] public Vector3 QSpecialOffset { get; private set; }
    
    [field: Header("{Movement Values}")]
    [field: SerializeField] public float WalkingSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    
    [field: Header("{Basic Attacks}")]
    [field: SerializeField] public List<ComboAttack> PunchAttacks { get; private set; }
    [field: SerializeField] public List<ComboAttack> KickAttacks { get; private set; }
    
    
    public InputReader InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterHealth CharacterHealth { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Start()
    {
        InputReader = GetComponent<InputReader>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        CharacterHealth = GetComponent<CharacterHealth>();
        
        CharacterHealth.CharacterGotNormalHitEvent += HandleTakeDamage;
        CharacterHealth.OnDie += HandleDeath;
        
        SwitchState(new PlayerFreeLookState(this));
    }
    
    private void HandleTakeDamage()
    {
        if (CharacterHealth.currentHealth <= 0)
        {
            CharacterHealth.OnDie -= HandleTakeDamage;
            return;
        }
        
        SwitchState(new PlayerHitReaction(this));
    }
    
    private void HandleDeath()
    {
        SwitchState(new PlayerDeadState(this));
        CharacterHealth.OnDie -= HandleDeath;
    }

    private void OnDestroy()
    {
        CharacterHealth.CharacterGotNormalHitEvent -= HandleTakeDamage;
        CharacterHealth.OnDie -= HandleDeath;
    }
}
