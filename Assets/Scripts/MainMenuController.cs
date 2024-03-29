﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;
    
    [SerializeField]
    private GameObject[] _panels;
    
    private MainMenuPanelType _currentPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        HideAllPanels();
        _currentPanel = MainMenuPanelType.Main;
        ShowPanel(_currentPanel);
    }

    private void HideAllPanels()
    {
        foreach (GameObject panel in _panels)
        {
            panel.SetActive(false);
        }
    }

    public void ShowPanel(int panelIndex)
    {
        if (panelIndex == 1)
        ShowPanelOnly(_currentPanel = (MainMenuPanelType)panelIndex);
        Debug.Log($"ShowPanel: {_currentPanel}");
    }

    private void ShowPanel(MainMenuPanelType panelType)
    {
        _panels[(int)panelType].SetActive(true);
    }

    private void ShowPanelOnly(MainMenuPanelType panelType)
    {
        HideAllPanels();
        _panels[(int)panelType].SetActive(true);
    }

    public void NewGameOnClick()
    {
        // ShowPanelOnly(_currentPanel = MainMenuPanelType.New);

        GameController.Instance.ResetGame();
        GameController.Instance.GameScene = GameScene.Spaceport;
    }

    public void OptionsOnClick()
    {
        ShowPanelOnly(_currentPanel = MainMenuPanelType.Options);
    }
    
    public void CreditsOnClick()
    {
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.R))
        {
            ShowPanelOnly(_currentPanel = MainMenuPanelType.Credits);
            return;
        }
        
        GameController.Instance.GameScene = GameScene.Credits;
    }

    public void BackOnClick()
    {
        switch (_currentPanel)
        {
            case MainMenuPanelType.New:
                // Debug.Log($"Pressed Back from New");
                ShowPanelOnly(_currentPanel = MainMenuPanelType.Main);
                break;
            case MainMenuPanelType.Options:
                // Debug.Log($"Pressed Back from Options");
                ShowPanelOnly(_currentPanel = MainMenuPanelType.Main);
                break;
            case MainMenuPanelType.Credits:
                // Debug.Log($"Pressed Back from Credits");
                ShowPanelOnly(_currentPanel = MainMenuPanelType.Main);
                break;
        }
    }
    
    public void ExitOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
