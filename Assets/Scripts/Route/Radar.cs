using Assets.Scripts.Route;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{

    [SerializeField]
    RadarEncounter meteorEncounterPrefab;

    [SerializeField]
    RadarEncounter solarFlareEncounterPrefab;

    [SerializeField]
    RadarEncounter gammaRayEncounterPrefab;

    [SerializeField]
    GameObject outpostPrefab;

    private RectTransform routeLine;
    private RectTransform radarTransform;
    private float routeLength;

    public RectTransform RadarTransform
    {
        get 
        { 
            if(radarTransform == null)
                radarTransform = transform.Find("Radar").GetComponent<RectTransform>();
            return radarTransform; 
        }
        set { radarTransform = value; }
    }

    public float RouteLength
    {
        get { return routeLength; }
        set 
        {
            routeLength = value;
            CreateRouteLine();
        }
    }


    void Awake()
    {
        radarTransform = transform.Find("Radar").GetComponent<RectTransform>();
    }

    public void CreateRouteLine()
    {
        routeLine = transform.Find("RouteLine").GetComponent<RectTransform>();
        routeLine.sizeDelta = new Vector2(100, routeLength * RadarController.Instance.RouteLengthMultiplier) / 2;

        var encounters = MissionController.Instance.Route.EncountersOnRoute;

        foreach (var encounter in encounters)
        {
            RadarEncounter radarEncounter = null;

            switch (encounter.EncounterType)
            {
                case EncounterType.SolarFlare:
                    radarEncounter = GameObject.Instantiate(solarFlareEncounterPrefab, routeLine);
                    break;
                case EncounterType.Meteor:
                    radarEncounter = GameObject.Instantiate(meteorEncounterPrefab, routeLine);
                    break;
                case EncounterType.GammaRay:
                    radarEncounter = GameObject.Instantiate(gammaRayEncounterPrefab, routeLine);
                    break;
            }

            radarEncounter.encounter = encounter;
            radarEncounter.transform.Translate(new Vector2(0, encounter.Position * RadarController.Instance.RouteLengthMultiplier));
        }

        var outpost = GameObject.Instantiate(outpostPrefab, routeLine);
        outpost.transform.Translate(new Vector2(0, RouteLength * RadarController.Instance.RouteLengthMultiplier + 50));

    }
}
