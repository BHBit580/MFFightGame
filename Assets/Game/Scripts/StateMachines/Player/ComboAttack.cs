using System;
using UnityEngine;

[Serializable]
public class ComboAttack
{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; } = 0.1f;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float KnockBack { get; private set; }
    
    [field: SerializeField, Range(0f, 1f)] //At minimum what percent of animation will play 
    public float MinimumNormalizedTime { get; private set; } = 0.5f;

    [field: SerializeField, Range(0f, 1f)] // If player press button within combo time then next animation will play 
    public float ComboWindowTime { get; private set; } = 0.6f;
}