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
    private void Awake()
    {
        GetAllComponents();
        
        _menuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
    }

    private void GetAllComponents()
    {
        _menuPanel = transform.GetChild(0).gameObject;
    }

    public void ToggleMenu()
    {
        _menuPanel.SetActive(!_menuPanel.activeInHierarchy);
    }

    public void Options()
    {
        
    }

    public void ExitToMainMenu()
    {
        GameController.Instance.GameScene = GameScene.MainMenu;
    }
}
