using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts.Controls;
using System.Linq;

public class BridgeControls : MonoBehaviour, BaseControl
{
    [SerializeField]
    private TextMeshProUGUI etaValue;

    ShipSystem shipSystem;
    RoomController roomController;




    public float AirLevel { get; set; }
    public float EnergyLevel { get; set; }

    void Start()
    {
        shipSystem = SystemController.Instance.ShipSystems.FirstOrDefault(x => x.SystemType == SystemType.Bridge);
    }

    void Update()
    {
        AirLevel = shipSystem.AirLevel;
    }
}
