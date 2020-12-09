using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController
{

    public int amountOfSystems;
    public ShipSystem[] ShipSystems;

    public static SystemController Instance;

    public SystemController()
    {
        if (Instance == null)
            Instance = this;

        //I HATE this line soooo much, but it seems liek the best way to do it, weirdly enough.
        //TODO: See if you can get it to work some other way if you use that GetValue thing later...
        amountOfSystems = SystemType.GetNames(typeof(SystemType)).Length;

        ShipSystems = new ShipSystem[amountOfSystems];

        //I do feel like there should be possible here to make this into a loop, and have index and enumtype just increase by one each time.
        //And also send them to ShipSystem, and then have ShipSystem send it on to the correct subclass based on the type of the enum...
        ShipSystems[0] = new ReactorSystem();


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
