using Assets.Scripts;
using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorSystem : ShipSystem
{
    private Ship ship;

    private float FuelCost;
    public float Efficiency
    {
        get { return _efficiency; }
        set { _efficiency = Mathf.Clamp(value, 0.00f, 1.00f); }
    }
    private float _efficiency;

    public int CapacityLevel
    {
        get { return _capacityLevel; }
        set { _capacityLevel = Mathf.Clamp(value, 1, 3); }
    }
    private int _capacityLevel;

    private bool IsRetrograde;
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
        CapacityLevel = 2;
        IsRetrograde = false;
        energyOutput = ship.CapacitorBottleNeck / 3;
        Efficiency = 1.00f;

        EnergyWanted = 0;
    }

public void BurnsFuel()
    {
        ship.Fuel -= CapacityLevel * FuelCost;
    }

    public void ProdEnergy()
    {
        energyOutput = energyOutput * CapacityLevel * Efficiency;

        ship.Capacitor += energyOutput;
    }

    public void ProdSpeed()
    {
        ship.Speed = CapacityLevel;
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
