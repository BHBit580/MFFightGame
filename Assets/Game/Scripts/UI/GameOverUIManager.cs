using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO playerDeath;
    [SerializeField] private VoidEventChannelSO enemyDeath;
    [SerializeField] private GameObject enemyWinUI;
    [SerializeField] private GameObject playerWinUI;
    [SerializeField] private GameObject playerButtons;
    private void Awake()
    {
        enemyWinUI.SetActive(false);
        playerWinUI.SetActive(false);
        enemyDeath.RegisterListener(ShowPlayerWinUI);
        playerDeath.RegisterListener(ShowEnemyWinUI);
    }

    private void ShowEnemyWinUI()
    {
        playerButtons.SetActive(false);
        enemyWinUI.SetActive(true);
    }
    
    private void ShowPlayerWinUI()
    {
        playerWinUI.SetActive(true);
        enemyWinUI.SetActive(false);
    }


    private void OnDestroy()
    {
        enemyDeath.UnregisterListener(ShowPlayerWinUI);
        playerDeath.UnregisterListener(ShowEnemyWinUI);
    }
}
