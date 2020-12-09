using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainframeSystem : ShipSystem
{
    float CapacitorMax;
    float CapacitorHolds;

    bool isCharging;

    public MainframeSystem()
    {
    
    }

    public void MainframeUpdate()
    {
        GetEnergyNeeded();

        DivideEnergy();

        SendEnergyOut();
    }

    public void GetEnergyNeeded()
    {

    }

    public void DivideEnergy()
    {

    }

    public void SendEnergyOut()
    {

    }
}
