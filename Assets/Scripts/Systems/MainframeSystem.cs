using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainframeSystem : ShipSystem
{
    Ship ship;

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

        if (energyNeeded > 0)
        {
            var energyFragment = DivideEnergy(energyNeeded);

            SendEnergyOut(energyFragment);
        }
    }
    /// <summary>
    /// Goes through all active shipsystems and gathers energyWanted into a float
    /// </summary>
    /// <returns>A float of energyNeeded from all active systems</returns>
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
    /// <summary>
    /// Takes in float energyNeeded, decreases the ship Capacitor & increases MainframeStsten.CurrentEnergy
    /// in a while loop until it has enough energy OR has hit the Ship.CapacitorBottleNeck OR ship.Capacitor is zero.
    /// </summary>
    /// <param name="energyNeeded"></param>
    /// <returns>CurrentEnergy divided by energyNeeded</returns>
    public float DivideEnergy(float energyNeeded)
    {
        while(energyNeeded >= ship.CapacitorBottleNeck && ship.Capacitor > 0)
        {
            CurrentEnergy++;
            ship.Capacitor--;
        }

        return CurrentEnergy / energyNeeded;
    }
    /// <summary>
    /// Goes through all active systems, and sets their CurrentEnergy =
    /// energyFragment mult * EnergyWanted of that system.
    /// This means that each system will receive energy proportional to their need.
    /// </summary>
    /// <param name="energyFragment"></param>
    public void SendEnergyOut(float energyFragment)
    {
        var activeSystems = SystemController.Instance.GetActiveSystems();

        foreach (var activeSystem in activeSystems)
        {
            activeSystem.CurrentEnergy = energyFragment * activeSystem.EnergyWanted;
        }
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
