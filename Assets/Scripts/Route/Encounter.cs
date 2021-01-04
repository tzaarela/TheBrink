using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter
{
    public float Position { get; set; }

    public float Severity { get; set; }

    public EncounterType EncounterType { get; set; }

    public bool HasTriggered { get; set; }

    public Encounter(float _encounterPosition, float _severity, EncounterType encounterType)
    {

        Position = _encounterPosition;

        Severity = _severity;

        //I'm just going to set this here, later on we will have other better constructors that will be able to set encounters
        //to different types.
        EncounterType = encounterType;
    }
    public void Execute()
    {
        switch (EncounterType)
        {
            case EncounterType.Meteor:
                Debug.Log("The ship is got struck by a meteor!");
                RoomController.Instance.CreateBreachInRooms(Severity, 3);
                break;

            case EncounterType.SolarFlare:
                Debug.Log("The ship is exposed to a dangerous solar flare!");
                RoomController.Instance.CreateFireInRooms(Severity, 3);
                break;
            case EncounterType.GammaRay:
                Debug.Log("The ship is exposed to a gamma-ray!");
                RoomController.Instance.CreateElectricFailureInRooms(Severity, 3);
                break;
            default:
                Debug.Log("The EncounterType wasn't one that the method could recognize.");
                break;
        }
    }
}
