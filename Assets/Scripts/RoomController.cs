using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<Room> Rooms { get; set; }

    private void Start()
    {
        CreateRooms();
    }

    private void Update()
    {
        
    }

    public List<Room> CreateRooms()
    {
        List<Room> rooms = new List<Room>()
        {
            new Room(RoomType.Reactor),
            new Room(RoomType.AirLock),
            new Room(RoomType.Bridge),
            new Room(RoomType.CargoBay),
            new Room(RoomType.MedBay),
            new Room(RoomType.MainFrame),
            new Room(RoomType.MainBattery),
            new Room(RoomType.CrewQuarters),
        };

        return rooms;
    }
}
