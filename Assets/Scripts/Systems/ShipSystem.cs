using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystem
{
    public SystemType SystemType;
    public SystemState SystemState;

    public float EnergyWanted = 0;
    public float currentEnergy = 0;
    public float EnergyToMaintain = 0;

    public ShipSystem()
    {
        SystemState = SystemState.IsOn;
    }

    public void Run()
    {
        switch (SystemType)
        {
            case SystemType.Bridge:

                break;
            
            case SystemType.Mainframe:

                break;
            
            case SystemType.MainBattery:

                break;
            
            case SystemType.LifeSupport:

                break;
            
            case SystemType.Medbay:

                break;
            
            case SystemType.Reactor:

                var shipSystem = this as ReactorSystem;
                shipSystem.ReactorUpdate();
                
                break;
            
            case SystemType.CargoBay:

                break;
            
            case SystemType.Corridors:

                break;
            
            default:
                
                break;
        }
    }

    public void RebootingSystem()
    {

    }
    public void DiagnosingSystem()
    {

    }
}
