using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public float Length { get; set; }

    public int EncounterAmount { get; set; }

    public float ShipPosition { get; set; }

    public List<Encounter> EncountersOnRoute { get; set; }

    /// <summary>
    /// This constructor creates a route with a number of Encounters evenly spaced along it's length.
    /// The reason there is no encounter at the end of the route is because that is where the next Starport is obv.//DJ
    /// </summary>
    /// <param name="_routeLength"></param>
    /// <param name="_encounterAmount"></param>
    public Route(float _routeLength, int _encounterAmount, float _dangerLevel)
    {
        float _distanceToNextEncounter;

        Length = _routeLength;

        EncounterAmount = _encounterAmount;

        _distanceToNextEncounter = Length / EncounterAmount + 1;

        for(int i = 1; i <= EncounterAmount; i++)
        {
            _distanceToNextEncounter *= i;

            Encounter encounter = new Encounter(_distanceToNextEncounter, _dangerLevel);

            EncountersOnRoute.Add(encounter);
        }
    }
}
