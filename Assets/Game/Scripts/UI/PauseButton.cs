using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private GameObject pauseScreenUI;
    [SerializeField] private GameObject playerInputs;
    [SerializeField] private float gameTimeScale = 0.002f;

    private ColorAdjustments colorAdjustments;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (volume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            this.colorAdjustments = colorAdjustments;
        }
        
        pauseScreenUI.SetActive(true);               //Doing this to register events  
        pauseScreenUI.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 1f;
        colorAdjustments.saturation.value = 0f;
    }

    private void Start()
    {
        button.onClick.AddListener(OnClickPauseButton);
    }

    private void OnClickPauseButton()
    {
        playerInputs.SetActive(false);
        pauseScreenUI.SetActive(true);
        Time.timeScale = gameTimeScale;
        colorAdjustments.saturation.value = -100f;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClickPauseButton);
    }
}
