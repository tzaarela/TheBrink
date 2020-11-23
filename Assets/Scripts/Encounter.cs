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
        /*
        TODO: Add a way to randomize the position here...
        Or might need to do that in Route actually since that is able to also see total length to figure fractions that it should be random within?
        */
        Position = _encounterPosition;

        /*
        TODO. Later on, fix so that this is sent by route here, and then from here to RoomController, and from RoomController to Room...
        And in the end is set as Severity of Hazard//DJ
        */
        Severity = 10;

        /*
        TODO: Later on, need to figure way to have encounterType chose itself with random.
        Will also need way to affect that random, certain areas will have a higher chance of different randoms yes?
        */
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
