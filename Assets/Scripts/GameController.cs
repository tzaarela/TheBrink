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
    public bool isDebuging;
    [SerializeField] private GameScene _beginDebugScene;


    private Action onTransitionComplete;

    public GameScene GameScene 
    { 
        get => gameScene; 
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

        // DEBUG ONLY
        if (isDebuging)
        {
            GameScene = _beginDebugScene;
        }
            
        else
        {
            GameScene = GameScene.Start;
            gameScene = GameScene.MainMenu;
        }
    }

    public void SwitchScene(GameScene gameScene)
    {
        TransitionController transitionController = TransitionController.Instance;

        switch (gameScene)
        {
            case GameScene.MainMenu:
                TransitionController.Instance.FadeOut();
                TransitionController.Instance.onFadedOut += () =>
                {
                    AudioController.instance.StopAllSound();
                    AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.MainMenu);
                    SceneManager.LoadScene("MainMenuScene");
                    TransitionController.Instance.FadeIn();
                };
                break;

            case GameScene.Mission:
                TransitionController.Instance.FadeOut();
                TransitionController.Instance.onFadedOut += () =>
                {
                    var sceneIndex = SceneManager.GetSceneByBuildIndex(2);
                    AsyncOperation op = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);

                    op.completed += (AsyncOperation o) =>
                    {

                        AudioController.instance.StopAllSound();
                        AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.Mission);
                        AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Ambient, BGMClipType.MissionAmbient);

                        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MissionScene"));
                        TransitionController.Instance.FadeIn();

                        RoomController.Instance.CreateRooms();
                        SystemController.Instance.CreateShipSystems(ship);
                        CrewController.Instance.CreateShipCrew(crew);
                        SystemWindowController.Instance.InitSystemContent();
                        MissionController.Instance.isInitialised = true;

                        if (!isDebuging)
                            MissionController.Instance.Route = ship.missionContract.route;

                    };
                };

                break;

            case GameScene.SpaceportNoIntro:
                HandleLoginComplete();
                break;

            case GameScene.Spaceport:
                TransitionController.Instance.FadeOut();
                TransitionController.Instance.onFadedOut += () =>
                {
                    TransitionController.Instance.FadeIn();
                    onTransitionComplete += HandleLoginComplete;
                    transitionController.PlayLogin(onTransitionComplete);
                };
                //AudioController.instance.StopAllBGM();
                break;

            case GameScene.Start:
                AudioController.instance.StopAllSound();
                AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.MainMenu);
                break;

            case GameScene.GameOver:

                TransitionController.Instance.FadeOut();
                TransitionController.Instance.onFadedOut += () =>
                {
                    SceneManager.LoadScene("GameOverScene");
                    TransitionController.Instance.FadeIn();
                };
                break;
            case GameScene.Docking:
                SceneManager.LoadScene("DockingScene");
                TransitionController.Instance.FadeIn();
                break;
            default:
                break;
        }
    }

    public void ResetGame()
    {
        crew.Reset();
        ship.Reset();
    }

    private void HandleLoginComplete()
    {
        TransitionController.Instance.FadeOut();
        TransitionController.Instance.onFadedOut += () =>
        {
            AudioController.instance.StopAllSound();
            AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.Spaceport);
            SceneManager.LoadScene("SpaceportScene");
            TransitionController.Instance.FadeIn();
        }; 
        
    }
}
