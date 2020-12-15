using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorSystem : ShipSystem
{
    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }

    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public float AirLevel { get; set; }

    public CorridorSystem()
    {
        SystemState = SystemState.IsOn;
        SystemType = SystemType.Corridors;

        EnergyWanted = 0;

        /*
         * It needs a list of all corridors & airlocks.
         * Goes through the list, and updates the airvalues etc on each.
         * Remember that they update their air in Life Support, so here we only need to update the amount.
         * 
         * And then, we need to be able to close and open doors from here. There is a bool I think? Right?
         * 
         * But how does it know which door? And this method should maybe be on ALL shipSystems right?
         * 
         * Have we decided if corridoes need energy or not?
         */
    }

    public void Run()
    {
        throw new System.NotImplementedException();
    }

    public void SetEnergyWanted()
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
