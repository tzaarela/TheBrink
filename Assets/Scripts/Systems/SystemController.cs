using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{

    [Header("Life Support")]
    public float optimalAirLevel = 95f;
    public float airProduceCost = 3f;


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
        //I HATE this line soooo much, but it seems liek the best way to do it, weirdly enough.
        //TODO: See if you can get it to work some other way if you use that GetValue thing later...
        amountOfSystems = SystemType.GetNames(typeof(SystemType)).Length;

        ShipSystems = new ShipSystem[amountOfSystems];

        //I do feel like there should be possible here to make this into a loop, and have index and enumtype just increase by one each time.
        //And also send them to ShipSystem, and then have ShipSystem send it on to the correct subclass based on the type of the enum...
        ShipSystems[0] = new ReactorSystem();
        ShipSystems[1] = new MainframeSystem();
        ShipSystems[2] = new MainBatterySystem();
        ShipSystems[3] = new LifeSupportSystem();
        ShipSystems[4] = new BridgeSystem();
        ShipSystems[5] = new MedbaySystem();
        ShipSystems[6] = new CargoHoldSystem();
        ShipSystems[7] = new CorridorSystem();
    }

    public void ShipSystemUpdate()
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
