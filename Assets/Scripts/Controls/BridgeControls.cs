using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts.Controls;
using System.Linq;

public class BridgeControls : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI etaValue;

    [SerializeField]
    private TextMeshProUGUI distanceValue;

    IShipSystem shipSystem;
    RoomController roomController;

    SystemController systemController;

    void Start()
    {
        systemController = SystemController.Instance;
        shipSystem = SystemController.Instance.GetSystemOfType(SystemType.Bridge);
    }

    void Update()
    {
        var wholeMinutes = systemController.estimatedTimeToArrival / 60;
        var minutes = Mathf.FloorToInt(wholeMinutes);
        var seconds = systemController.estimatedTimeToArrival % 60;

        etaValue.text = $"{minutes} Min  {Mathf.CeilToInt(seconds)} Sec"; 
        BridgeSystem bridgeSystem = (BridgeSystem)shipSystem;
        distanceValue.text = Mathf.CeilToInt(bridgeSystem.DistanceToStarport).ToString() + " AU";
    }
}
