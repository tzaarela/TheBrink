using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float AirLevel { get; set; }
    public float RadiationLevel { get; set; }
    public bool HasElectricity { get; set; }
    //Added this trait here, think we are going to need it, hope it's okay? - DJ
    public float RoomHealth { get; set; }
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
        RoomHealth = 100;
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

    public void RepairRoom(HazardType _hazardTypeToRepair)
    {
        //TODO: Talk to JS or EN about this
                //Suggestion 1: Will this float be seen in Unity for ease of manipulation? To figure if repair goes to slowly?
                //Suggestion 2: Could this be turned into a property of each crewmember later on maybe?
        float _crewMemberRepairSkill = 5;

        foreach(Hazard Hazard in Hazards)
        {
            if (Hazard.HazardType == _hazardTypeToRepair)
            {
                Hazard.SeverityAmount -= _crewMemberRepairSkill;
                //TODO: Figure out upper bounds of this, can a fire burn too fast?
                
                //I think this will mean that the crewmember will do one repair action, and then get sent out of the method.
                return;
            }
        }

        Debug.Log("The crewmember couldn't find a hazard of the type they were told to repair");
        /*
         * Okay, so, to fix a room.
         * And I need a way to find out the damages on the room.
         * So, suppose that we put a function in crewmembers, that lets them repair a room.
         * As of now, the rooms are actually not damaged, severityAmount is it's own thing.
         * So the only way to repair now is to decrease SeverityAmount.
         * So the Room will need to find the hazard, and then change that hazards SeverityAmount...
         * So it will need to do this once for every hazard?
         * It will need to go through its own list?
         * 
         * So, first, check if there is a crewmember in the room...
         * if it is, send in their "skill" value in repair.
         * And maybe what type of hazard they want to work against?
         * And then in repair, it checks for that hazard, and then lets them fight it?
         * (decreases its severityAmount for now)
         * So using this method will look like Room.RepairRoom(hazardType _hazardToRepair) ?
        */
    }
}
