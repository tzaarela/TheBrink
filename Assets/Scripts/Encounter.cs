using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public float Position { get; set; }
    
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
        TODO: Later on, need to figure way to have encounterType chose itself with random.
        Will also need way to affect that random, certain areas will have a higher chance of different randoms yes?
        */
        EncounterType = EncounterType.SmallMeteorSwarm;
    }
}
