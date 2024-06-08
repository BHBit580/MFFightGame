using System.Collections.Generic;
using UnityEngine;

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
    [field: SerializeField] public List<ComboAttack> BasicAttacks { get; private set; }
    
    
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
        
        SwitchState(new PlayerFreeLookState(this));
    }
    
    
}
