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

    public void Awake()
    {
        if(GameController.Instance == null)
        {
            var gameController = (GameController)ScriptableObject.CreateInstance(typeof(GameController));

            gameController.Init(ship, crew);
            DontDestroyOnLoad(gameObject);
        }
    }
}
