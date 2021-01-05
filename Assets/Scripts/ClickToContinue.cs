using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToContinue
{
    public GameScene swapToScene;
    KeyCode keyCode;

    public ClickToContinue(GameScene swapToScene, KeyCode keyCode)
    {
        this.swapToScene = swapToScene;
        this.keyCode = keyCode;
    }

    public ClickToContinue(GameScene swapToScene)
    {
        this.swapToScene = swapToScene;
        this.keyCode = KeyCode.None;
    }

    public void CheckInput()
    {
        if (keyCode == KeyCode.None && Input.anyKey)
            GameController.Instance.GameScene = swapToScene;

        else if (Input.GetKeyDown(keyCode))
            GameController.Instance.GameScene = swapToScene;
    }
}
