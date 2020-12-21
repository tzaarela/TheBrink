using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShipSystem 
{
    SystemType SystemType { get; set; }
    PowerState PowerState  { get; set; }

    float AirLevel { get; set; }

    float EnergyWanted { get; set; }

    float CurrentEnergy { get; set; }

    float CurrentEnergyInSystem { get; set; }

    float EnergyToMaintain { get; set; }

    void Run();

    void Update();

    void SetEnergyWanted();

    void Reboot();

    void RunDiagnostic();
}
