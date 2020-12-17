using Assets.Scripts;
using Assets.Scripts.Crew;
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
    public TransitionController transitionController;
    
    [Header("DEBUG")]
    public bool _debuging;
    [SerializeField] private GameScene _beginDebugScene;

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

    public void Init(Ship ship, Crew crew, TransitionController transitionController)
    {
        this.ship = ship;
        this.crew = crew;
        this.transitionController = transitionController;

        // TODO DEBUG ONLY
        if (_debuging)
            SwitchScene(_beginDebugScene);
        // SwitchScene(GameScene.InMission);
    }

    public void SwitchScene(GameScene gameScene)
    {
        switch (gameScene)
        {
            case GameScene.InMainMenu:
                {
                    SceneManager.LoadScene("MainMenuScene");
                    transitionController.RunTransitionAnimation(gameScene);
                    break;

                }
            case GameScene.InMission:
                {
                    
                    transitionController.RunTransitionAnimation(gameScene);

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
                    SceneManager.LoadScene("SpaceportScene");
                    transitionController.RunTransitionAnimation(gameScene);
                    break;
                }
            default:
                break;
        }
    }
}
