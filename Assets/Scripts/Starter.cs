using Assets.Scripts;
using Assets.Scripts.Crew;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public SystemController systemController;

    [SerializeField]
    private Ship ship;

    [SerializeField]
    private Crew crew;

    [SerializeField]
    private GameController gameController;

    public void Awake()
    {
        if(GameController.Instance == null)
        {
            gameController.Init(ship, crew);
            DontDestroyOnLoad(gameObject);
        }
    }
}
