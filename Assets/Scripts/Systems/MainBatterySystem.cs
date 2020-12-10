﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBatterySystem : ShipSystem
{
    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public MainBatterySystem()
    {
        SystemState = SystemState.IsOn;
    }

    public void Run()
    {
        //throw new System.NotImplementedException();
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