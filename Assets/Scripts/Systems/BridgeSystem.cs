﻿using Assets.Scripts;
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

        SystemRoom = this.rooms.FirstOrDefault(x => x.SystemType == SystemType.Bridge);

        route = MissionController.Instance.Route;

        CurrentEnergy = 50;

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
        base.Run();
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


    public override void Reboot()
    {
        throw new System.NotImplementedException();
    }

    public override void RunDiagnostic()
    {
        throw new System.NotImplementedException();
    }

    
}