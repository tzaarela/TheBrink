using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToContinue : MonoBehaviour
{
    [SerializeField]
    GameScene swapToScene;

    void Update()
    {
        if (Input.anyKey)
            GameController.Instance.GameScene = swapToScene;
    }
}
