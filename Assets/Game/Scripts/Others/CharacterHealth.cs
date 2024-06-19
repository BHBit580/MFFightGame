using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float currentHealth = 100;
    private bool isInvulnerable;
    
    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public event Action CharacterGotNormalHitEvent;
    public event Action CharacterHealthDecreased;

    public event Action OnDie;

    public void DealDamage(float damageAmount , bool specialAttack = false)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        if ((int)currentHealth == 0)
        {
            Debug.Log("fire");
            OnDie?.Invoke();
        }
        
        
        if (isInvulnerable) return;
        
        if (specialAttack == false)
        {
            CharacterGotNormalHitEvent?.Invoke();
        }
        
        currentHealth -= damageAmount;
        CharacterHealthDecreased?.Invoke();
    }
}
