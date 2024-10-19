using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private CharacterHealth characterHealth;


    private void Awake()
    {
        UpdateHealthUI();
        characterHealth.CharacterHealthDecreased += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        healthBar.fillAmount = (float)characterHealth.currentHealth / 100;
    }
    
    private void OnDestroy()
    {
        characterHealth.CharacterHealthDecreased -= UpdateHealthUI;
    }
}
