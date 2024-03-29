﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route
{
    public float Length { get; set; }
    public float ShipPosition { get; set; }
    public int EncounterAmount { get; set; }

    public List<Encounter> EncountersOnRoute { get; set; }

    public Route()
    {

    }

    /// <summary>
    /// This constructor creates a route with a number of Encounters evenly spaced along it's length.
    /// The reason there is no encounter at the end of the route is because that is where the next Starport is obv.//DJ
    /// </summary>
    /// <param name="_routeLength"></param>
    /// <param name="_encounterAmount"></param>
    public Route(float _routeLength, int _encounterAmount, float _dangerLevel)
    {
        EncountersOnRoute = new List<Encounter>();

        float _distanceToNextEncounter;
        
        //TODO: Think we can delete this now that Encounters checks against the var Position on the Ship instead?
        ShipPosition = 0;

        Length = _routeLength;

        EncounterAmount = _encounterAmount;

        _distanceToNextEncounter = Length / EncounterAmount;

        for(int i = 1; i <= EncounterAmount; i++)
        {
            var position = _distanceToNextEncounter * i;

            var encounterType = (EncounterType)Random.Range(0, 3);

            Encounter encounter = new Encounter(position, _dangerLevel, encounterType);

            EncountersOnRoute.Add(encounter);
        }
    }
}
