using System;
using UnityEngine;

[Serializable]
public class EnemyAttack
{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; } = 0.1f;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float KnockBack { get; private set; }
}