using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainframeSystem : ShipSystem
{
    Ship ship;

    float CapacitorMax;
    float CapacitorHolds;

    bool isCharging;

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public MainframeSystem(Ship ship)
    {
        this.ship = ship;
        
    }

    public void Run()
    {
        var energyNeeded = GetEnergyNeeded();

        DivideEnergy(energyNeeded);

        SendEnergyOut();
    }

    public float GetEnergyNeeded()
    {
        float totalEnergyNeeded = 0;

        var activeSystems = SystemController.Instance.GetActiveSystems();

        foreach (var activeSystem in activeSystems)
        {
            totalEnergyNeeded += activeSystem.EnergyWanted;
        }
        return totalEnergyNeeded;
    }

    public void DivideEnergy(float energyNeeded)
    {

    }

    public void SendEnergyOut()
    {

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
