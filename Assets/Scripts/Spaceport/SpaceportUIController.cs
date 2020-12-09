using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceportUIController : MonoBehaviour
{
    public static SpaceportUIController instance;

    private SpaceportPanelController[] _panelControllers;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        GetAllComponents();
    }

    private void Start()
    {
        SetAllComponents();
        ShowPanel(SpaceportTabType.Contracts);
    }

    private void GetAllComponents()
    {
    }
    
    private void SetAllComponents()
    {
        _panelControllers = new SpaceportPanelController[3];
        _panelControllers[(int)SpaceportTabType.Contracts] = SpaceportContractsController.instance;
        _panelControllers[(int)SpaceportTabType.Barrack] = SpaceportBarrackController.instance;
        _panelControllers[(int)SpaceportTabType.Workshop] = SpaceportWorkshopController.instance;
    }

    private void HideAllPanels()
    {
        foreach (SpaceportPanelController panelController in _panelControllers)
        {
            panelController.SetPanel(false);
        }
    }

    public void ShowPanel(SpaceportTabType tabType)
    {
        HideAllPanels();
        _panelControllers[(int)tabType].SetPanel(true);
    }
}
