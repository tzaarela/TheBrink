using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSystem : ShipSystem
{
    Route route;
    Ship ship;

    //TODO: Fix, should this pass reference instead? And also do so to SystemController.
    //float timeMax = 0.1f;

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

        route = MissionController.Instance.Route;
        ship = ShipController.Instance.Ship;

        EnergyWanted = 0;
    }

    public void Run()
    {
        UpdateETA();
        UpdateTimeToRetro();
        UpdateTimeToEncounter();

        SetEnergyWanted();
    }

    public void UpdateETA()
    {
        EstimatedTimetoArrival = (((route.Length - route.ShipPosition) / ship.Speed) * 0.1f);
        //TODO: Really needs to check this with Saarela
    }

    public void UpdateTimeToRetro()
    {
        //Take current speed, see, how many times you can remove that speed before it goes within benchmarks.
        //Then, take that number, divide by update time yes?
    }

    public void UpdateTimeToEncounter()
    {

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