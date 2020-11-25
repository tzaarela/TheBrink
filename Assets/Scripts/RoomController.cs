using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> roomGameObjects;

    private List<Room> rooms;

    public List<Room> Rooms
    {
        get { return rooms; }
        set { rooms = value; }
    }


    public static RoomController Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            rooms = new List<Room>();
            CreateRooms();
        }
        else
        {
            Destroy(this);
        }
    }

    public void CreateRooms()
    {
        rooms.AddRange(roomGameObjects.Select(x => x.GetComponent<Room>()));
    }

    public void CreateBreachInRoom(float severity)
    {
        //int _indexOf 1;

        float _severityAmount = severity;
        //I do wonder if I've been stupid here, maybe I don't need to send with roomtype,
        //Maybe that is something that RoomController should look into instead,

        

        //Rooms[_indexOf].CreateHazard(HazardType.Breach, _severityAmount);
    }
}
