using Assets.Scripts;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BridgeSystem : ShipSystem
{
    Route route;
    Ship ship;

    //TODO: Fix, should this pass reference instead? And also do so to SystemController, so we might interact with it from there?
    //float timeMax = 0.1f;

    //Navigationvariables
    float DistanceToStarport;
    float DistanceToRetrogradeBurn;
    float DisanceToNextEncounter;

    //Time variables
    public float EstimatedTimetoArrival { get; set; }
    public float TimeUntilRetrogradeBurn { get; set; }
    public float TimeUntilNextEncounter { get; set; }

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }
    public float AirLevel { get; set; }

    private Room systemRoom;
    private List<Room> rooms;

    public BridgeSystem(Ship ship, List<Room> rooms)
    {
        this.ship = ship;
        this.rooms = rooms;
        SystemState = SystemState.IsOn;
        SystemType = SystemType.Bridge;

        systemRoom = this.rooms.FirstOrDefault(x => x.RoomType == RoomType.Bridge);

        route = MissionController.Instance.Route;

        EnergyWanted = 0;
    }

    public void Run()
    {
        AirLevel = systemRoom.oxygenLevel;
        EstimatedTimetoArrival = UpdateETA();
        TimeUntilRetrogradeBurn = UpdateTimeToRetro();
        TimeUntilNextEncounter = UpdateTimeToEncounter();

        //So, I leave this here atm so I can turn the method below into returning a float later.
        //EnergyWanted = 
        SetEnergyWanted();
    }

    public float UpdateETA()
    {
        return (((route.Length - route.ShipPosition) / ship.speed) * MissionController.TICK_TIMER_MAX);
    }

    public float UpdateTimeToRetro()
    {
        //Take current speed, see, how many times you can remove that speed before it goes within benchmarks.
        //Then, take that number, divide by update time yes?

        return 10;
    }

    public float UpdateTimeToEncounter()
    {
        //Go through the route, look at all encounters in array,
        //find the encounters that are still bool "on" and has the lowest position.
        //Take that encounter and compare it to figure things out.

        return 10;
    }

    public void SetEnergyWanted()
    {
        //TODO: Set this method to return a float instead?
        //Based on what Saarela mentioned that might be the best way to handle this sort of things.
        EnergyWanted = EnergyToMaintain;
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