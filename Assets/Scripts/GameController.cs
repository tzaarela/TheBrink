using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; set; }

    public GameState GameState { get; set; }

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        GameState = GameState.InMainMenu;
    }


    // Update is called once per frame
    void Update()
    {
        switch (GameState)
        {
            case GameState.InMainMenu:
                break;
            case GameState.InMission:
                break;
            case GameState.InGameOver:
                break;
            default:
                break;
        }
    }
}
