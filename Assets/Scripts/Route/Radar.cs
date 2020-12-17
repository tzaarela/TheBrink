using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{

    [SerializeField]
    GameObject enemyEncounterPrefab;
    
    [SerializeField] private RectTransform routeLine;
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
        // routeLine = transform.Find("RouteLine").GetComponent<RectTransform>();
        routeLine.sizeDelta = new Vector2(35, routeLength * RadarController.Instance.RouteLengthMultiplier) / 2;

        var encounters = MissionController.Instance.Route.EncountersOnRoute;

        foreach (var encounter in encounters)
        {
            var radarEncounter = GameObject.Instantiate(enemyEncounterPrefab, routeLine);

            radarEncounter.transform.Translate(new Vector2(0, encounter.Position * RadarController.Instance.RouteLengthMultiplier));
        }

    }
}
