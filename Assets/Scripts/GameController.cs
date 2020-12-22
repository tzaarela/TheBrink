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
    
    [Header("DEBUG")]
    public bool _debuging;
    [SerializeField] private GameScene _beginDebugScene;


    private Action onTransitionComplete;

    public GameScene GameScene { get => gameScene; 
        set 
        {
            gameScene = value;
            SwitchScene(gameScene);
        } 
    }

    private GameScene gameScene = GameScene.Start;

    public void Init(Ship ship, Crew crew)
    {
        Instance = this;
        this.ship = ship;
        this.crew = crew;

        // TODO DEBUG ONLY
        if (_debuging)
            SwitchScene(_beginDebugScene);
        else
            SwitchScene(GameScene.Start);
    }

    public void SwitchScene(GameScene gameScene)
    {
        TransitionController transitionController = TransitionController.Instance;

        switch (gameScene)
        {
            case GameScene.MainMenu:
                AudioController.instance.StopAllSound();
                AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.MainMenu, 0.2f);
                SceneManager.LoadScene("MainMenuScene");
                break;

            case GameScene.Mission:
                var sceneIndex = SceneManager.GetSceneByBuildIndex(2);

                AsyncOperation op = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);

                op.completed += (AsyncOperation o) =>
                {
                    AudioController.instance.StopAllSound();
                    AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.Mission, 0.2f);
                    AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Ambient, BGMClipType.MissionAmbient, 0.1f);
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName("MissionScene"));

                    if (!_debuging)
                        MissionController.Instance.Route = ship.missionContract.route;

                    RoomController.Instance.CreateRooms();
                    SystemController.Instance.CreateShipSystems(ship);
                    CrewController.Instance.CreateShipCrew(crew);

                };
                break;

            case GameScene.SpaceportNoIntro:
                HandleLoginComplete();
                break;

            case GameScene.Spaceport:
                onTransitionComplete += HandleLoginComplete;
                transitionController.PlayLogin(onTransitionComplete);
                AudioController.instance.StopAllBGM();
                break;

            case GameScene.Start:
                AudioController.instance.StopAllSound();
                AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.MainMenu, 0.2f);
                break;

            case GameScene.GameOver:
                AudioController.instance.StopAllSound();
                SceneManager.LoadScene("GameOverScene");
                break;

            default:
                break;
        }
    }

    private void HandleLoginComplete()
    {
        AudioController.instance.StopAllSound();
        AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.Spaceport, 0.1f);
        SceneManager.LoadScene("SpaceportScene");
        
    }
}
