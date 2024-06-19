using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class OnClickStartButton : MonoBehaviour
{
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private float loadDelay;
    
    private string sceneName = "Game";
    
    public void StartGame()
    {
        TransitionManager.Instance().Transition(sceneName , transitionSettings , loadDelay);
    }
}
