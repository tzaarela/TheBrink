using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemController : MonoBehaviour
{

    [Header("BridgeSystem")]

    [Header("MainframeSystem")]

    [Header("MainBatterySystem")]

    [Header("Life Support")]
    public float optimalAirLevel = 95f;
    public float airProduceCost = 3f;

    [Header("MedbaySystem")]

    [Header("ReactorSystem")]

    [Header("CargoHoldSystem")]

    [Header("CorridorsSystem")]


    private int amountOfSystems;
    public ShipSystem[] ShipSystems;

    public static SystemController Instance;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void Start()
    {
        amountOfSystems = SystemType.GetNames(typeof(SystemType)).Length;
        Ship ship = ShipController.Instance.Ship;


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
