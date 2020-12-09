using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystem : MonoBehaviour
{
    public SystemType SystemType;
    public SystemState SystemState;

    public float EnergyWanted = 0;
    public float EnergyGained = 0;
    public float EnergyToMaintain = 0;

    public ShipSystem()
    {
        SystemState = SystemState.IsOn;
    }

    public void Update()
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
                
                //ReactorSystem.ReactorUpdate();
                
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
