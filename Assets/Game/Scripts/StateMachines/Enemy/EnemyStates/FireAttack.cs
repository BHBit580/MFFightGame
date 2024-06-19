using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    [SerializeField] private float damage = 2.2f;
    [SerializeField] private float timeBetweenDamage = 0.2f;

    private float damageTimer;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent(out CharacterHealth characterHealth))
            {
                damageTimer += Time.deltaTime;
                
                if (damageTimer >= timeBetweenDamage)
                {
                    characterHealth.DealDamage(damage, false);
                    damageTimer = 0f;
                }
            }
        }
    }
}