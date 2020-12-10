using Assets.Scripts;
using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorSystem : ShipSystem
{
    private Ship ship;

    private float FuelCost;
    private float Efficiency;
    private bool IsRetrograde;
    private float capacityLevel;
    private float energyOutput;

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public ReactorSystem(Ship ship)
    {
        this.ship = ship;
        SystemType = SystemType.Reactor;
        SystemState = SystemState.IsOn;
        FuelCost = 1;
        capacityLevel = 2;
        IsRetrograde = false;
        energyOutput = ship.CapacitorBottleNeck / 3;
        Efficiency = 1.00f;
    }

    public void BurnsFuel()
    {
        ship.Fuel -= capacityLevel * FuelCost;
    }

    public void ProdEnergy()
    {
        energyOutput = energyOutput * capacityLevel * Efficiency;

        ship.Capacitor += energyOutput;
    }

    public void ProdSpeed()
    {
        ship.Speed = capacityLevel;
    }

    public void Run()
    {
        if (ship.Fuel > 0)
        {
            BurnsFuel();
            ProdEnergy();
            ProdSpeed();
        }
        else
            ConsoleController.instance.PrintToConsole("Reactor has no more fuel");
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
