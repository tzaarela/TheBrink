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

    ShipSystem shipSystem;
    RoomController roomController;

    void Start()
    {
        shipSystem = SystemController.Instance.ShipSystems.FirstOrDefault(x => x.SystemType == SystemType.Bridge);
    }
}
