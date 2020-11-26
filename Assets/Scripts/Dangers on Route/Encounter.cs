using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public float Position { get; set; }

    public float Severity { get; set; }

    public EncounterType EncounterType { get; set; }

    public bool HasTriggered { get; set; }

    public Encounter(float _encounterPosition)
    {

        Position = _encounterPosition;

        Severity = 10;

        EncounterType = EncounterType.SmallMeteorSwarm;
    }
    public void Execute()
    { 
        switch(EncounterType) 
        {
            case EncounterType.SmallMeteorSwarm:
                Debug.Log("The ship is struck by a small swarm of meteors!");
                
                RoomController.Instance.CreateBreachInRoom(Severity);
                break;

            case EncounterType.SolarFlare:
                Debug.Log("The ship is exposed to a dangerous solar flare!");
                break;

            default:
                Debug.Log("The EncounterType wasn't one that the method could recognize.");
                break;
        }
    }
}
