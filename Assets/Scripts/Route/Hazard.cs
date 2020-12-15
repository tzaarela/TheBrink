using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard
{
    public float SeverityAmount
    {
        get { return severityAmount; }
        set { severityAmount = Mathf.Clamp(value, 0f, 100f); }
    }
    private float severityAmount;
    public HazardType HazardType { get; set; }

    public bool IsFinished { get; set; }

    private Room _hazardRoom;

    public Hazard(HazardType hazardType, float severityAmount, Room hazardRoom)
    {
        HazardType = hazardType;
        SeverityAmount = severityAmount;
        _hazardRoom = hazardRoom;
    }
    //Initialize here, add function, that way called only once, called from room. create hazard. So causes inital damage
    public void UpdateHazard()
    {
        switch (HazardType)
        {
            case HazardType.Breach:
                RoomWithHullBreach();
                break;
            case HazardType.Fire:
                RoomOnFire();
                break;
            case HazardType.RoomSpecific:
                RoomSpecificHazard();
                break;
            default:
                Debug.Log("Hazard is of unknown type. HazardType is: " + this.HazardType);
                break;
        }

        if (SeverityAmount <= 0)
        {
            IsFinished = true;
        }
    }

    ///<summary>
    ///RoomHasBreach() decreases the AirLevel in the room by a percentage based on size of breach.
    ///AirLevel decreases by less the less air is in the room.
    ///</summary>
    public void RoomWithHullBreach()
    {        
        //TODO: I messed up here, I need to fix so that, in this case, you will decrease the severity when you repair the room (since it is a crack in the hull).

        _hazardRoom.OxygenLevel -= ((SeverityAmount * _hazardRoom.OxygenLevel) / 100);

        Debug.Log("Air is leaving the room, current airlevel is " + _hazardRoom.OxygenLevel);
    }

    public void RoomOnFire()
    {
        FireGrows();

        FireConsumes();

        FireBurns();
    }

    /// <summary>
    /// This method will increase or decrease the SeverityAmount by a percentage based on AirLevel.
    /// Is AirLevel less than the SeverityAmount, the SeverityAmount will decrease by the difference div by 100. 
    /// </summary>
    public void FireGrows()
    {

        SeverityAmount += SeverityAmount * ( (_hazardRoom.OxygenLevel - SeverityAmount) / 100 );
    }

    /// <summary>
    /// This method lets the fire consume air as a fraction of the SeverityAmount
    /// A bigger fire will therefore consume more air.
    /// </summary>
    public void FireConsumes()
    {
        _hazardRoom.OxygenLevel -= SeverityAmount / 100;
    }

    /// <summary>
    /// This method does nothing right now except print a debug line.
    /// I added it though in case we wanted the rooms to be damaged in some way if the fire gets too hot,
    /// or if we want the fire to hurt the crewmembers or some such.
    /// </summary>
    public void FireBurns()
    {
        if(SeverityAmount >= _hazardRoom.RoomHealth / 2)
        {   
            //Debug.Log("The fire is burning hot enough that this room will be damaged!");
        }
    }

    public void RoomSpecificHazard() 
    {
    switch(_hazardRoom.RoomType)
        {
            case RoomType.Reactor:
                break;
            case RoomType.MainBattery:
                break;
            case RoomType.MedBay:
                break;
            default:
                Debug.Log("hazard of type " + this.HazardType + "is of a specified RoomType that doesn't exist yet");
                break;
        }
    }
}
