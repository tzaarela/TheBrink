using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField]
    Ship ship;

    public void Awake()
    {
        var gameController = (GameController)ScriptableObject.CreateInstance(typeof(GameController));
        gameController.Init(ship);
        DontDestroyOnLoad(gameObject);
    }

}
