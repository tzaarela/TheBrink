using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private List<Room> _rooms;
    
    [SerializeField]
    private List<GameObject> roomGameObjects;

    public List<Room> Rooms
    {
        get { return _rooms; }
        set { _rooms = value; }
    }

    public static RoomController Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            _rooms = new List<Room>();
            CreateRooms();
        }
        else
        {
            Destroy(this);
        }
    }

    public void CreateRooms()
    {
        _rooms.AddRange(roomGameObjects.Select(x => x.GetComponent<Room>()));
    }

    /// <summary>
    /// this method creates an ongoing breach hazard in all corridors of the ship.
    /// It also, when created, decreases the health of the room by the severity, to indicate how the hull has been breached.
    /// </summary>
    /// <param name="severity"></param>
    public void CreateBreachInRoom(float severity)
    {
        var randomIndex = UnityEngine.Random.Range(0, Rooms.Count - 1);

        Rooms[randomIndex].CreateHazard(HazardType.Breach, severity);
        Rooms[randomIndex].RoomHealth -= severity;
    }
         
    public void CreateFireInRoom(float severity)
    {
        var randomIndex = UnityEngine.Random.Range(0, Rooms.Count - 1);
        ConsoleController.instance.PrintToConsole("WARNING! Fire in " + Rooms[randomIndex].name + "! ", 0.04f, false);

        Rooms[randomIndex].CreateHazard(HazardType.Fire, severity);
    }
}
