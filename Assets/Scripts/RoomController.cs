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

    public void CreateBreachInRoom(float severity)
    {
        /*
         * Chose a room from Rooms
         * tell that room
         * to create a hazard of type depending on encounter
         * with severityAmount = to severity
         * 
        */
    }
}
