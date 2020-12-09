using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainframeSystem : ShipSystem
{
    float CapacitorMax;
    float CapacitorHolds;

    bool isCharging;

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public MainframeSystem()
    {
    
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

    public void Run()
    {
        GetEnergyNeeded();

        DivideEnergy();

        SendEnergyOut();
    }

    public void Reboot()
    {
        throw new System.NotImplementedException();
    }

    public void RunDiagnostic()
    {
        throw new System.NotImplementedException();
    }
}
