﻿using Assets.Scripts;
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
    public float AirLevel { get; set; }

    public ReactorSystem(Ship ship)
    {
        this.ship = ship;
        SystemType = SystemType.Reactor;
        SystemState = SystemState.IsOn;
        FuelCost = 1;
        CapacityLevel = 2;
        IsRetrograde = false;
        //Wait, surely this is bizzarre? What was I thinking? Having an energyoutput that is LOWER than the bottleneck?
        energyOutput = ship.capacitorBottleNeck / 3;
        Efficiency = 1.00f;

        EnergyWanted = 0;
    }

    public void Run()
    {
        if (ship.fuel > 0)
        {
            BurnsFuel();
            ProdEnergy();
            ProdSpeed();
        }
        else
        {
            ConsoleController.instance.PrintToConsole("Reactor has no more fuel");
            //Remove this later, if you implement the other way for the ship speed and travel and momentum to happen.
            ship.speed = 0;
        }
    }

    public void BurnsFuel()
    {
        ship.fuel -= CapacityLevel * FuelCost;
    }

    public void ProdEnergy()
    {
        energyOutput = energyOutput * CapacityLevel * Efficiency;

        ship.capacitor += energyOutput;
    }

    public void ProdSpeed()
    {
        ship.speed = CapacityLevel;
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
