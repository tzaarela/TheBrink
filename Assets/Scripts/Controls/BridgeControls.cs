using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts.Controls;
using System.Linq;

public class BridgeControls : MonoBehaviour, IControls 
{
    [SerializeField]
    private TextMeshProUGUI etaValue;

    [SerializeField]
    private TextMeshProUGUI distanceValue;

    IShipSystem shipSystem;
    RoomController roomController;

    SystemController systemController;

    IShipSystem IControls.shipSystem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void UpdateUI()
    {
        var wholeMinutes = systemController.estimatedTimeToArrival / 60;
        var minutes = Mathf.FloorToInt(wholeMinutes);
        var seconds = systemController.estimatedTimeToArrival % 60;

        etaValue.text = $"{minutes} Min  {Mathf.CeilToInt(seconds)} Sec"; 
        BridgeSystem bridgeSystem = (BridgeSystem)shipSystem;
        distanceValue.text = Mathf.CeilToInt(bridgeSystem.DistanceToStarport).ToString() + " AU";
    }

    public void Init()
    {
        systemController = SystemController.Instance;
        shipSystem = SystemController.Instance.GetSystemOfType(SystemType.Bridge);
    }
}
