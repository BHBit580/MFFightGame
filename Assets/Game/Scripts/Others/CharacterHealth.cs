using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int currentHealth = 100;
    private bool isInvulnerable;
    
    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public event Action CharacterGotNormalHitEvent;
    public event Action CharacterHealthDecreased;

    public void DealDamage(int damageAmount , bool specialAttack = false)
    {
        if (isInvulnerable) return;
        
        if (specialAttack == false)
        {
            CharacterGotNormalHitEvent?.Invoke();
        }
        
        currentHealth -= damageAmount;
        CharacterHealthDecreased?.Invoke();
    }
}
