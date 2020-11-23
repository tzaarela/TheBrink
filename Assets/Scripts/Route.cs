using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public float RouteLength { get; set; }

    public int EncounterAmount { get; set; }

    public float ShipPosition { get; set; }

    public List<Encounter> EncountersOnRoute { get; set; }

    /// <summary>
    /// This constructor creates a route with a number of Encounters evenly spaced along it's length. //DJ
    /// </summary>
    /// <param name="_routeLength"></param>
    /// <param name="_encounterAmount"></param>
    public Route(float _routeLength, int _encounterAmount)
    {
        //TODO: Should this be broken into to parts? Maybe we can create routes without needing to create all of this encounters until later?
        //Have routes and then also ExecuteRoutes() as another function that creates the routes?
        float _distanceToNextEncounter;

        RouteLength = _routeLength;

        EncounterAmount = _encounterAmount;

        _distanceToNextEncounter = RouteLength / EncounterAmount + 1;

        for(int i = 1; i <= EncounterAmount; i++)
        {
            _distanceToNextEncounter *= i;

            /*
            TODO: I need to know how to handle the protection level here,
            should I be sending this in some other way? //DJ
            */

            Encounter encounter = new Encounter(_distanceToNextEncounter);

            EncountersOnRoute.Add(encounter);
        }
        
    }
}
