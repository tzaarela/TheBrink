using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SystemController", menuName = "SystemController")]
public class SystemController : ScriptableObject
{
    [Header("BridgeSystem")]

    [Header("MainframeSystem")]

    [Header("MainBatterySystem")]

    [Header("Life Support")]
    public float optimalOxygenLevel = 95f;
    public float oxygenProduceCost = 3f;

    [Header("MedbaySystem")]
    public float healingAmount = 1f;

    [Header("ReactorSystem")]

    [Header("CargoHoldSystem")]

    [Header("CorridorsSystem")]

    public ShipSystem[] ShipSystems;

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
        var rooms = RoomController.Instance.Rooms;
        var amountOfSystems = SystemType.GetNames(typeof(SystemType)).Length;

        ShipSystems = new ShipSystem[amountOfSystems];

        ShipSystems[0] = new ReactorSystem(ship, rooms);
        ShipSystems[1] = new MainframeSystem(ship, rooms);
        ShipSystems[2] = new MainBatterySystem(rooms);
        ShipSystems[3] = new LifeSupportSystem(rooms);
        ShipSystems[4] = new BridgeSystem(ship, rooms);
        ShipSystems[5] = new MedbaySystem(rooms);
        ShipSystems[6] = new CargoHoldSystem(rooms);
        ShipSystems[7] = new CorridorSystem(rooms);
    }

    public List<ShipSystem> GetActiveSystems()
    {
        return ShipSystems.Where(x => x.SystemState == SystemState.IsOn).ToList();
    }

    public void ShipSystemUpdate()
    {
        var activeSystems = GetActiveSystems();

        foreach (var activeSystem in activeSystems)
        {
            activeSystem.Run();
        }
    }
}
