using Assets.Scripts;
using Assets.Scripts.Crew;
using Assets.Scripts.Tweening.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameController", menuName = "GameController")]
public class GameController : ScriptableObject
{
    public static GameController Instance;

    public Ship ship;
    public Crew crew;

    private Action onTransitionComplete;

    public GameScene GameScene { get => gameScene; 
        set 
        {
            gameScene = value;
            SwitchScene(gameScene);
        } 
    }

    private GameScene gameScene = GameScene.InMainMenu;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Init(Ship ship, Crew crew)
    {
        this.ship = ship;
        this.crew = crew;
        

        //Debug.
        //SwitchScene(GameScene.InMission);
    }

    public void SwitchScene(GameScene gameScene)
    {
        TransitionController transitionController = TransitionController.Instance;

        switch (gameScene)
        {
            case GameScene.InMainMenu:
                {
                    SceneManager.LoadScene("MainMenuScene");
                    break;

                }
            case GameScene.InMission:
                {
                    var sceneIndex = SceneManager.GetSceneByBuildIndex(2);

                    AsyncOperation op = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);

                    op.completed += (AsyncOperation o) =>
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MissionScene"));
                        RoomController.Instance.CreateRooms();
                        SystemController.Instance.CreateShipSystems(ship);
                        CrewController.Instance.CreateShipCrew(crew);
                    };

                    break;
                }
            case GameScene.InSpaceport:
                {
                    onTransitionComplete += HandleLoginComplete;
                    transitionController.PlayLogin(onTransitionComplete);

                    break;
                }
            default:
                break;
        }
    }

    private void HandleLoginComplete()
    {
        SceneManager.LoadScene("SpaceportScene");
    }
}
