using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float AirLevel { get; set; }
    public float RadiationLevel { get; set; }
    public bool HasElectricity { get; set; }
    public RoomType RoomType { get; set; }
    public List<Hazard> Hazards { get; set; }

    private Door door;

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

    public void CreateHazard(HazardType hazardType, float severityAmount)
    {
        Hazard hazard = new Hazard(hazardType, severityAmount);

        Hazards.Add(hazard);
    }

    public void CreateDoor()
    {
        door = new Door(_doorPosition);
    }
}
