using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts;
using UnityEngine.PlayerLoop;

public class SpaceportUIController : MonoBehaviour
{
    public static SpaceportUIController instance;

    private SpaceportPanelController[] _panelControllers;

    [SerializeField] private TMP_Text _cashText;

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
        Init();
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

    private void Init()
    {
        // ShowPanel(SpaceportTabType.Contracts);
        ShowPanel(SpaceportTabType.Workshop);   // TODO Debug Only.
        UpdateCash(0);
    }

    private void HideAllPanels()
    {
        foreach (SpaceportPanelController panelController in _panelControllers)
        {
            panelController.SetRootPanel(false);
        }
    }

    public void ShowPanel(SpaceportTabType tabType)
    {
        HideAllPanels();
        _panelControllers[(int)tabType].SetRootPanel(true);
    }

    public void Depart()
    {
        GameController.Instance.GameScene = GameScene.InMission;
    }

    public void UpdateCash(int increaseAmount)
    {
        GameController.Instance.ship.cash += increaseAmount;
        _cashText.text = "$" + GameController.Instance.ship.cash.ToString("N0");
    }
}
