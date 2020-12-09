using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoHoldSystem : ShipSystem
{
    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    float Load;
    //Decide here, what should the load be? Look up how much the ship should carry.
    float MaxLoad;
    
    public CargoHoldSystem()
    {

    }

    public void Run()
    {
        throw new System.NotImplementedException();
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
