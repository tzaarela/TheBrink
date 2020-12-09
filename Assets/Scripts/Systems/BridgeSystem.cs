using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSystem : ShipSystem
{
    //Navigationvariables
    float DistanceToStarport;
    float DistanceToRetrogradeBurn;
    float DisanceToNextEncounter;

    //Time variables
    float EstimatedTimetoArrival;
    float TimeUntilRetrogradeBurn;
    float TimeUntilNextEncounter;

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public BridgeSystem()
    {
        SystemState = SystemState.IsOn;
    }

    public void Run()
    {
        UpdateETA();
        UpdateTimeToRetro();
        UpdateTimeToEncounter();
    }

    public void UpdateETA()
    {

    }

    public void UpdateTimeToRetro()
    {

    }

    public void UpdateTimeToEncounter()
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
