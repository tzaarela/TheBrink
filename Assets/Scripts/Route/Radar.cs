using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    [SerializeField]
    float routeLengthMultiplier;
    
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
            routeLine = transform.Find("RouteLine").GetComponent<RectTransform>();
            routeLength = value;
            routeLine.sizeDelta = new Vector2(4, routeLength * routeLengthMultiplier);
        }
    }


    void Awake()
    {
        radarTransform = transform.Find("Radar").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
