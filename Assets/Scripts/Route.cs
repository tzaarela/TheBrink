using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public float RouteLength { get; set; }
    public int EncounterAmount { get; set; }

    public List<Encounter> EncountersOnRoute { get; set; }

    Route(float _routeLength, int _encounterAmount)
    { 
        RouteLength = _routeLength;

        EncounterAmount = _encounterAmount;
    }
}
