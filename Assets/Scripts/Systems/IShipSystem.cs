using Assets.Scripts;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipSystem 
{
    Room SystemRoom { get; set; }

    SystemType SystemType { get; set; }
    PowerState PowerState  { get; set; }

    bool IsDepressurised { get; set; }

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
