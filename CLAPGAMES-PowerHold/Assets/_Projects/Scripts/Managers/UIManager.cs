using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("Reference")] [Space] [SerializeField]
    private INVEvents inv;

    [Header("Components")] [Space]
    public GameObject startPanel;
    public GameObject successPanel;
    public GameObject failPanel;

    private void Start()
    {
        PreparingPanels();
    }

    #region Game (UI) Settings

    private void PreparingPanels()
    {
        startPanel.SetActive(true);
        successPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    #endregion
    
    #region Button Functions

    public void OnClicked_StartButton()
    {
        inv.OnStartButtonClicked();
        startPanel.SetActive(false);
    }

    public void OnClicked_MainMenuButton()
    {
        
    }
    public void OnClicked_TryAgainButton()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
