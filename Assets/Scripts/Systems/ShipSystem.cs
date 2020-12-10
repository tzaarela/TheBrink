using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShipSystem 
{
    SystemType SystemType { get; set; }
    SystemState SystemState  { get; set; }

    float EnergyWanted { get; set; }

    float CurrentEnergy { get; set; }

    float EnergyToMaintain { get; set; }

    void Run();

    void SetEnergyWanted();

    void Reboot();

    void RunDiagnostic();
}
