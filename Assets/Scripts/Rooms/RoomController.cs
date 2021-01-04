using Assets.Scripts;
using Assets.Scripts.Rooms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> roomGameObjects;

    [SerializeField]
    private float airDrainLevel = 0.01f;

    private List<Room> _rooms;
    public List<Room> Rooms
    {
        get { return _rooms; }
        set { _rooms = value; }
    }

    public static RoomController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    public void UpdateRooms()
    {
        foreach (Room room in Rooms)
        {
            if (room.SystemType == SystemType.Corridors)
                continue;

            room.AirDrain(airDrainLevel);
            room.UpdateHazard();
            room.PresentCrewMembers
                .Where(x => x.isDead).ToList()
                .ForEach(x => x.Die());
        }
    }

    public void CreateRooms()
    {
        _rooms = new List<Room>();
        _rooms.AddRange(roomGameObjects.Select(x => x.GetComponent<Room>()));
    }

    /// <summary>
    /// this method creates an ongoing breach hazard in all corridors of the ship.
    /// It also, when created, decreases the health of the room by the severity, to indicate how the hull has been breached.
    /// </summary>
    /// <param name="severity"></param>
    public void CreateBreachInRooms(float severity, int numberOfAffectedRooms)
    {
        var roomIndices = GetRandomRoomIndices();
        var random = new System.Random();

        for (int i = 0; i < numberOfAffectedRooms; ++i)
        {
            var index = roomIndices[i];
            ConsoleController.instance.PrintToConsole("WARNING! Hullbreach in " + Rooms[index].name + "! ", 0.01f, false);
            Rooms[index].CreateHazard(HazardType.Breach, severity);
            Rooms[index].data.TakeDamage(severity);
            Rooms[index].PresentCrewMembers.ForEach(x => x.TakeDamage(20));
        }
    }
         
    public void CreateFireInRoom(float severity, int roomIndex)
    {
        ConsoleController.instance.PrintToConsole("WARNING! Fire in " + Rooms[roomIndex].name + "! ", 0.01f, false);
        Rooms[roomIndex].CreateHazard(HazardType.Fire, severity);
    }

    public void CreateFireInRooms(float severity, int numberOfAffectedRooms)
    {
        var roomIndices = GetRandomRoomIndices();
        var random = new System.Random();

        for (int i = 0; i < numberOfAffectedRooms; ++i)
        {
            var index = roomIndices[i];
            ConsoleController.instance.PrintToConsole("WARNING! Fire in " + Rooms[index].name + "! ", 0.01f, false);

            Rooms[index].CreateHazard(HazardType.Fire, severity);
        }
    }

    public void CreateElectricFailureInRoom(float severity, int roomIndex)
    {
        ConsoleController.instance.PrintToConsole("WARNING! Electrical failure in " + Rooms[roomIndex].name + "! ", 0.01f, false);
        Rooms[roomIndex].CreateHazard(HazardType.ElectricFailure, severity);
    }

    public void CreateElectricFailureInRooms(float severity, int numberOfAffectedRooms)
    {
        var roomIndices = GetRandomRoomIndices();
        var random = new System.Random();

        for (int i = 0; i < numberOfAffectedRooms; ++i)
        {
            var index = roomIndices[i];
            ConsoleController.instance.PrintToConsole("WARNING! Electrical failure in " + Rooms[index].name + "! ", 0.01f, false);
            Rooms[index].CreateHazard(HazardType.ElectricFailure, severity);
            
        }
    }

    private int[] GetRandomRoomIndices()
    {
        var roomIndices = Enumerable.Range(0, Rooms.Count - 1).ToArray();
        var random = new System.Random();

        for (int i = 0; i < Rooms.Count - 1; ++i)
        {
            int randomIndex = random.Next(roomIndices.Length);
            int temp = roomIndices[randomIndex];
            roomIndices[randomIndex] = roomIndices[i];
            roomIndices[i] = temp;
        }

        return roomIndices;
    }
}
