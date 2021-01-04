using Assets.Scripts;
using Assets.Scripts.Rooms;
using Assets.Scripts.Systems;
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
    float DistanceToRetrogradeBurn;
    float DisanceToNextEncounter;

    //Time variables
    public float DistanceToStarport { get; set; }
    public float EstimatedTimetoArrival { get; set; }
    public float TimeUntilRetrogradeBurn { get; set; }
    public float TimeUntilNextEncounter { get; set; }

    private List<Room> rooms;
    private SystemController systemController;

    public BridgeSystem(Ship ship, List<Room> rooms, SystemController systemController)
    {
        this.ship = ship;
        this.rooms = rooms;
        this.systemController = systemController;

        PowerState = PowerState.IsOn;
        SystemType = SystemType.Bridge;

        SystemRoom = this.rooms.FirstOrDefault(x => x.RoomType == SystemType.Bridge);

        route = MissionController.Instance.Route;

        //I set EnergyToMaintain to zero on Bridge, seeing as this system will not be able to be turned off, and should not take any energy.
        EnergyToMaintain = 0;
        EnergyWanted = 0;
    }
    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }

    public override void Run()
    {
        EstimatedTimetoArrival = GetETA();
        DistanceToStarport = GetDistanceLeft();
        TimeUntilRetrogradeBurn = UpdateTimeToRetro();
        TimeUntilNextEncounter = UpdateTimeToEncounter();

        //So, I leave this here atm so I can turn the method below into returning a float later.
        //EnergyWanted = 
        SetEnergyWanted();

        CurrentEnergyInSystem = CurrentEnergy;
    }

    public float GetDistanceLeft()
    {
        return route.Length - route.ShipPosition;
    }

    public float GetETA()
    {
        float eta = ((route.Length - route.ShipPosition) / ship.speed);
        systemController.estimatedTimeToArrival = eta;
        return eta; 
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

    public override void SetEnergyWanted()
    {
        //TODO: Set this method to return a float instead?
        //Based on what Saarela mentioned that might be the best way to handle this sort of things.
        EnergyWanted = EnergyToMaintain;
    }

    public override void Reboot()
    {
        throw new System.NotImplementedException();
    }

    public override void RunDiagnostic()
    {
        throw new System.NotImplementedException();
    }

    
}