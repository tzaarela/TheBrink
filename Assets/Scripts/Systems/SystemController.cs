﻿using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "SystemController", menuName = "SystemController")]
public class SystemController : ScriptableObject
{
    [Header("BridgeSystem")]
    public float estimatedTimeToArrival = 0f;

    [Header("MainframeSystem")]
    public float energyPortion = 0.5f;

    [Header("MainBatterySystem")]

    [Header("Life Support")]
    public float optimalOxygenLevel = 95f;
    public float oxygenProduced = 5f;
    public float oxygenProduceCost = 0.1f;

    [Header("MedbaySystem")]
    public float healingEnergyCost = 1f;
    public float healingAmount = 1f;

    [Header("ReactorSystem")]
    public float fuelCost = 0.1f;
    [Range(1, 3)]
    public int capacityLevel = 2;

    [Header("CargoHoldSystem")]

    [Header("CorridorsSystem")]

    public IShipSystem[] ShipSystems;
    private Debugger debugger;
    bool isDebug;

    //Add reference to all rooms (for airLevel in Life Support & DoorSystem).

    private static SystemController _instance;
    public static SystemController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (SystemController)Resources.FindObjectsOfTypeAll(typeof(SystemController)).FirstOrDefault();
            }

            return _instance;
        }
        private set { }
    }

    public void CreateShipSystems(Ship ship)
    {
        isDebug = GameController.Instance._debuging;
        debugger = Debugger.instance;
        var rooms = RoomController.Instance.Rooms;
        var amountOfSystems = SystemType.GetNames(typeof(SystemType)).Length;

        ShipSystems = new IShipSystem[amountOfSystems];

        ShipSystems[0] = new ReactorSystem(ship, rooms);
        ShipSystems[1] = new MainframeSystem(ship, rooms);
        ShipSystems[2] = new MainBatterySystem(rooms);
        ShipSystems[3] = new LifeSupportSystem(rooms);
        ShipSystems[4] = new BridgeSystem(ship, rooms, this);
        ShipSystems[5] = new MedbaySystem(rooms);
        ShipSystems[6] = new CargoHoldSystem(rooms);
        ShipSystems[7] = new CorridorSystem(rooms);
    }

    public List<IShipSystem> GetActiveSystems()
    {
        return ShipSystems.Where(x => x.PowerState == PowerState.IsOn).ToList();
    }

    public void ShipSystemUpdate()
    {
        foreach (var system in ShipSystems)
        {
            if (system.PowerState == PowerState.IsOn)
                system.Run();

            system.Update();

            if(isDebug)
                debugger.DebugPropertyValues(system);
        }

        debugger.isSetup = true;
    }
}
