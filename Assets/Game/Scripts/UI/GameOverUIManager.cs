using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO playerDeath;
    [SerializeField] private GameObject enemyWinUI;
    [SerializeField] private GameObject playerButtons;
    private void Awake()
    {
        enemyWinUI.SetActive(false);
        playerDeath.RegisterListener(ShowEnemyWinUI);
    }

    private void ShowEnemyWinUI()
    {
        playerButtons.SetActive(false);
        enemyWinUI.SetActive(true);
    }


    private void OnDestroy()
    {
        playerDeath.UnregisterListener(ShowEnemyWinUI);
    }
}
