using Assets.Scripts;
using Assets.Scripts.Route;
using Assets.Scripts.Tweening.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public static MissionController Instance { get; set; }

    public bool isInitialised;
    public const float TICK_TIMER_MAX = 0.1f;
    public Ship ship;
    public GameObject outpostReachedOverlay;
    
    private float tickTimer = 0;
    private Route route;
    private Action onTransitionComplete;
    private bool isFinished;

    public Route Route
    {
        get 
        { 
            if(route == null)
                route = new Route(1000, 10, 100);
            return route; 
        }
        set { route = value; }
    }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    public void Start()
    {
        ship = GameController.Instance.ship;
        ship.position = 0;
    }


    public void Update()
    {
        if (!isInitialised)
            return;

        if (isFinished)
            return;

        UpdateShipPosition();

        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer = 0;
            CrewController.Instance.UpdateCrewCommands();
            RoomController.Instance.UpdateRooms();
            SystemController.Instance.UpdateShipSystems();
            SystemWindowController.Instance.UpdateUISystemContent();
        }
    }
    public void StartMissions(Mission mission)
    {
        //Create route
    }

    public void GameOver()
    {
        GameController.Instance.GameScene = GameScene.GameOver;
    }

    private void UpdateShipPosition()
    {
        ship.position += ship.speed * Time.deltaTime;
        Route.ShipPosition = ship.position;

        if (route.ShipPosition >= route.Length && !isFinished)
        {
            FinishMission();
        }
    }

    private void FinishMission()
    {
        isFinished = true;
        outpostReachedOverlay.SetActive(true);
        onTransitionComplete += BackToOutpost;
        TransitionController.Instance.PlayOutpostReached(onTransitionComplete);
    }

    private void BackToOutpost()
    {
        outpostReachedOverlay.SetActive(false);
        GameController.Instance.GameScene = GameScene.Docking;
    }

}
