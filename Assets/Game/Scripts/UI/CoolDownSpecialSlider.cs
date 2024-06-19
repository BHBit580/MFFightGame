using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;


public class CoolDownSpecialSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image image;
    [SerializeField] private float speed = 1f;
    
    private OnScreenButton onScreenButton;
    private Button button;
    
    private bool hasDone;

    private void Start()
    {
        slider.value = 0f;
        button = GetComponent<Button>();
        onScreenButton = GetComponent<OnScreenButton>();
        onScreenButton.enabled = false;
        button.onClick.AddListener(ResetTimer);
        image.color = Color.white;
    }

    private void ResetTimer()
    {
        if(!hasDone) return;
        
        slider.value = 0f;
        image.color = Color.white;
        onScreenButton.enabled = false;
        hasDone = false;
    }

    private void Update()
    {
        if (slider.value <= 1 && !hasDone)
        {
            slider.value += Time.deltaTime * speed;
            button.interactable = false;
        }

        if (slider.value >= 1 && !hasDone)
        {
            onScreenButton.enabled = true;
            image.color = Color.cyan;
            hasDone = true;
            button.interactable = true;
        }
    }
    
}