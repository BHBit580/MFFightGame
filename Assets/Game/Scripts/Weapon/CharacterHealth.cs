using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public event Action CharacterGotNormalHitEvent;
    public event Action CharacterHealthDecreased;

    public void DealDamage(int damageAmount , bool specialAttack = false)
    {
        if (specialAttack == false)
        {
            CharacterGotNormalHitEvent?.Invoke();
        }
        
        currentHealth -= damageAmount;
        CharacterHealthDecreased?.Invoke();
    }
}
