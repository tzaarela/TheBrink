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
    [SerializeField] private TMP_Text _captainName;
    [SerializeField] private TMP_Text _spaceportName;

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
        GetAllComponents();
        Init();
    }

    private void GetAllComponents()
    {
        _panelControllers = new SpaceportPanelController[3];
        _panelControllers[(int)SpaceportTabType.Contracts] = SpaceportContractsController.instance;
        _panelControllers[(int)SpaceportTabType.Barrack] = SpaceportBarrackController.instance;
        _panelControllers[(int)SpaceportTabType.Workshop] = SpaceportWorkshopController.instance;
    }

    private void Init()
    {
        UpdateCash(0);
        _captainName.text = $"Ship Name: HTV Galloway\n" +
                            $"Captain: {GameController.Instance.ship.captainName}\n" +
                            $"Manufacturer Date: 2298-A0-B2";

        _spaceportName.text = $"Logged into: ";
        _spaceportName.text += (GameController.Instance.ship.missionContract == null)
                            ? $"Kata"
                            : $"{GameController.Instance.ship.GetContractName()}";

        GameController.Instance.ship.missionContract = null;

        if (!GameController.Instance.isDebuging)
        {
            ShowPanel(SpaceportTabType.Contracts);
            return;
        }
        // ShowPanel(SpaceportTabType.Contracts);
        ShowPanel(SpaceportTabType.Barrack);   // TODO Debug Only.
        // ShowPanel(SpaceportTabType.Workshop);   // TODO Debug Only.
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
        if (SpaceportContractsController.instance.HasContract())
            GameController.Instance.GameScene = GameScene.Mission;
    }

    public void UpdateCash(int increaseAmount)
    {
        GameController.Instance.ship.cash += increaseAmount;
        _cashText.text = "$" + GameController.Instance.ship.cash.ToString("N0");
    }
}
