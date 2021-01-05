using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    private GameObject _menuPanel;
    
    [SerializeField] private GameObject _systemFilesPanel;
    [SerializeField] private GameObject _optionsPanel;

    private void Awake()
    {
        GetAllComponents();
        
        _menuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameScene gameScene = GameController.Instance.GameScene;
            
            Debug.Log($"_gameScene: {gameScene}");
            if ((gameScene == GameScene.MainMenu && _menuPanel.activeInHierarchy) ||
                gameScene == GameScene.SpaceportNoIntro || gameScene == GameScene.Mission)
                ToggleMenu();
        }
    }

    private void GetAllComponents()
    {
        _menuPanel = transform.GetChild(0).gameObject;
    }

    public void ToggleMenu()
    {
        _menuPanel.SetActive(!_menuPanel.activeInHierarchy);

        if (_menuPanel.activeInHierarchy)
            ShowSystemFiles();
    }

    public void Options()
    {
        ShowOptions();
    }

    public void ExitToMainMenu()
    {
        GameController.Instance.GameScene = GameScene.MainMenu;
    }

    private void ShowSystemFiles()
    {
        _optionsPanel.SetActive(false);
        _systemFilesPanel.SetActive(true);
    }

    public void ConfirmOptions()
    {
        if (GameController.Instance.GameScene == GameScene.MainMenu)
            CloseOptions();
        else
            ShowSystemFiles();
    }

    private void ShowOptions()
    {
        _systemFilesPanel.SetActive(false);
        _optionsPanel.SetActive(true);
    }

    public void OpenOptions()
    {
        _menuPanel.SetActive(true);
        ShowOptions();
    }

    private void CloseOptions()
    {
        _menuPanel.SetActive(false);
    }
}
