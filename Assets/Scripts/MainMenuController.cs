using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _panels;

    private void Awake()
    {
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        HideAllPanels();
        ShowPanel(MainMenuPanelType.Main);
    }

    private void HideAllPanels()
    {
        foreach (GameObject panel in _panels)
        {
            panel.SetActive(false);
        }
    }

    private void ShowPanel(MainMenuPanelType panelType)
    {
        
    }

    public void NewOnClick()
    {
        
    }
    
    public void LoadOnClick()
    {
        
    }
    
    public void OptionsOnClick()
    {
        
    }
    
    public void CreditsOnClick()
    {
        
    }
    
    public void ExitOnClick()
    {
        Application.Quit();
    }
}
