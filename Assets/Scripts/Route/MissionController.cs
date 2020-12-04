using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public static MissionController Instance { get; set; }

    private const float TICK_TIMER_MAX = 0.1f;
    private float tickTimer = 0;
    private Route route;

    public Route Route
    {
        get 
        { 
            if(route == null)
                route = new Route(1000, 10, 10);
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
        
    }

    public void Update()
    {
        UpdateShipPosition();

        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer = 0;
            CheckEncounters();
            CrewController.Instance.UpdateCrew();
            UpdateRoom();
        }
    }


    private void UpdateShipPosition()
    {
        ShipController.Instance.Ship.Position += ShipController.Instance.Ship.Speed * Time.deltaTime;
        Route.ShipPosition = ShipController.Instance.Ship.Position;
    }

    private void UpdateRoom()
    {
        foreach(Room room in RoomController.Instance.Rooms)
        {
            room.UpdateHazard();
        }
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
