using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private CharacterHealth characterHealth;

    private void Update()
    {
        healthBar.fillAmount = (float)characterHealth.currentHealth / 100;
    }
}
