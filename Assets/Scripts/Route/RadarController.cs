using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour
{
    public static RadarController Instance { get; set; }

    [SerializeField]
    Radar radarPrefab;

    [SerializeField]
    Camera radarCamera;

    [SerializeField]
    Transform UI;
    
    Radar radarCanvas;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    public void Start()
    {
        radarCanvas = GameObject.Instantiate(radarPrefab, UI);
        radarCanvas.GetComponent<Canvas>().worldCamera = radarCamera;
        radarCanvas.transform.position = new Vector2(-2000, 0);
        radarCanvas.RouteLength = 1000;
    }
    
    void Update()
    {
        UpdateRadarPosition();
        RadarCameraFollow();
    }

    

    private void RadarCameraFollow()
    {
        if(radarCanvas != null)
            radarCamera.transform.position = new Vector3(radarCanvas.RadarTransform.position.x, radarCanvas.RadarTransform.position.y, -10);
    }

    private void UpdateRadarPosition()
    {
        if (radarCanvas != null)
        {
            radarCanvas.RadarTransform.position = new Vector2(-2000, MissionController.Instance.Route.ShipPosition);
        }

    }
}
