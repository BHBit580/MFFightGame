using System;
using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;


public class PauseScreenButtons : MonoBehaviour
{
    [SerializeField] private TransitionSettings transitionSettings;
    private string sceneName = "Home";

    
    public void GoToHome()
    {
        Time.timeScale = 1f;
        TransitionManager.Instance().Transition(sceneName , transitionSettings , 0);
    }

    public void ResetScene()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;
        TransitionManager.Instance().Transition(currentSceneName , transitionSettings , 0);
    }
    
}
