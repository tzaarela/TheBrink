using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameController", menuName = "GameController")]
public class GameController : ScriptableObject
{
    public static GameController Instance;

    public Ship ship;

    public GameScene GameScene { get => gameScene; 
        set 
        {
            gameScene = value;
            SwitchScene(gameScene);
        } 
    }

    private GameScene gameScene = GameScene.InMainMenu;
    private TransitionController transitionController;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    public void Init(Ship ship)
    {
        this.ship = ship;
        SystemController.Instance.CreateShipSystems(this.ship);
        RoomController.Instance.CreateRooms();
        CrewController.Instance.CreateShipCrew();
        transitionController = TransitionController.Instance;
    }

    public void SwitchScene(GameScene gameScene)
    {
        switch (gameScene)
        {
            case GameScene.InMainMenu:
                {
                    transitionController.RunTransitionAnimation("Menu Animation");
                    SceneManager.LoadScene("MainMenuScene");
                    break;

                }
            case GameScene.InMission:
                {
                    transitionController.RunTransitionAnimation("Menu Animation");
                    SceneManager.LoadScene("MissionScene");
                    break;
                }
            case GameScene.InSpaceport:
                {
                    transitionController.RunTransitionAnimation("Menu Animation");
                    SceneManager.LoadScene("SpaceportScene");
                    break;
                }
            default:
                break;
        }
    }
}
