﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float AirLevel { get; set; }
    public float RadiationLevel { get; set; }
    public bool HasElectricity { get; set; }
    public RoomType RoomType { get; set; }
    public List<Hazard> Hazards { get; set; }

    private Door _door;

    [SerializeField]
    private List<Transform> _neighbours;

    [SerializeField]
    private Transform _doorPosition;

    [SerializeField]
    private RoomType _roomType;

    public Room()
    {
        AirLevel = 100;
        RadiationLevel = 0;
        HasElectricity = true;
        RoomType = _roomType;
    }

    public void CreateHazard(HazardType hazardType, float severityAmount, Room currentRoom)
    {
        Hazard hazard = new Hazard(hazardType, severityAmount, currentRoom);

        Hazards.Add(hazard);
    }

    public void CreateDoor()
    {
        _door = new Door(_doorPosition);
    }

    public List<Transform> GetNeighbours()
    {
        return _neighbours;
    }
}
