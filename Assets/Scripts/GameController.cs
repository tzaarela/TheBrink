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

        // DEBUG ONLY
        if (isDebuging)
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
                TransitionController.Instance.FadeOut();
                TransitionController.Instance.onFadedOut += () =>
                {
                    AudioController.instance.StopAllSound();
                    AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.MainMenu);
                    SceneManager.LoadScene("MainMenuScene");
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
                onTransitionComplete += HandleLoginComplete;
                transitionController.PlayLogin(onTransitionComplete);
                AudioController.instance.StopAllBGM();
                break;

            case GameScene.Start:
                AudioController.instance.StopAllSound();
                AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.MainMenu);
                break;

            case GameScene.GameOver:

                TransitionController.Instance.FadeOut();
                TransitionController.Instance.onFadedOut += () =>
                {
                    AudioController.instance.StopAllSound();
                    SceneManager.LoadScene("GameOverScene");
                };
                break;
            case GameScene.Docking:
                TransitionController.Instance.FadeOut();
                TransitionController.Instance.onFadedOut += () =>
                {
                    SceneManager.LoadScene("DockingScene");
                };
                break;
            default:
                break;
        }
    }

    public void ResetGame()
    {
        foreach (CrewData crewData in crew.crewDataList)
        {
            crewData.Reset();
        }

        // for (int i = 0; i < crew.crewDataArray.Length; i++)
        // {
        //     crew.crewDataArray[i].Reset();
        // }
    }

    private void HandleLoginComplete()
    {
        TransitionController.Instance.FadeOut();
        TransitionController.Instance.onFadedOut += () =>
        {
            AudioController.instance.StopAllSound();
            AudioController.instance.PlayBGM(Assets.Scripts.Audio.AudioBGMType.Music, BGMClipType.Spaceport);
            SceneManager.LoadScene("SpaceportScene");
        }; 
        
    }
}
