using Assets.Scripts;
using Assets.Scripts.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public static MissionController Instance { get; set; }

    public Ship ship;

    public const float TICK_TIMER_MAX = 0.1f;
    private float tickTimer = 0;
    private Route route;

    public Route Route
    {
        get 
        { 
            if(route == null)
                route = new Route(1000, 100, 10);
            return route; 
        }
        set { route = value; }
    }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    public void Start()
    {
        ship = GameController.Instance.ship;
    }

    public void StartMissions(Mission mission)
    {
        //Create route
    }

    public void Update()
    {
        UpdateShipPosition();

        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer = 0;
            CheckEncounters();
            CrewController.Instance.UpdateCrewCommands();
            RoomController.Instance.UpdateRooms();
            SystemController.Instance.ShipSystemUpdate();
        }
    }

    private void UpdateShipPosition()
    {
        ship.position += ship.speed * Time.deltaTime;
        Route.ShipPosition = ship.position;
    }

    private void CheckEncounters()
    {
        foreach (Encounter encounter in route.EncountersOnRoute)
        {
            if (!encounter.HasTriggered && route.ShipPosition > encounter.Position)
            {
                
                Debug.Log("Encounter triggered!");
                
                encounter.HasTriggered = true;
                encounter.Execute();
            }
        }
    }

}
