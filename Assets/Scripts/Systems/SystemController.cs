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

    [Header("ReactorSystem")]

    [Header("CargoHoldSystem")]

    [Header("CorridorsSystem")]

    public ShipSystem[] ShipSystems;

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
        var amountOfSystems = SystemType.GetNames(typeof(SystemType)).Length;

        ShipSystems = new ShipSystem[amountOfSystems];

        ShipSystems[0] = new ReactorSystem(ship);
        ShipSystems[1] = new MainframeSystem(ship);
        ShipSystems[2] = new MainBatterySystem();
        ShipSystems[3] = new LifeSupportSystem();
        ShipSystems[4] = new BridgeSystem();
        ShipSystems[5] = new MedbaySystem();
        ShipSystems[6] = new CargoHoldSystem();
        ShipSystems[7] = new CorridorSystem();
    }

    public List<ShipSystem> GetActiveSystems()
    {
            return ShipSystems.Where(x => x.SystemState == SystemState.IsOn).ToList();
    }

    public void ShipSystemUpdate()
    {
        if(ShipSystems.Select(x => x == null).Count() == 0)
        {
            foreach(ShipSystem ShipSystem in ShipSystems)
            {
                if (ShipSystem.SystemState == SystemState.IsOn)
                {
                    ShipSystem.Run();
                }
            }
        }
    }
}
