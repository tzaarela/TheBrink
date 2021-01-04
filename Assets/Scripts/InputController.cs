using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool clickToContinue;
    public bool anyKey;
    public KeyCode keyCode;
    public GameScene gameScene;

    private ClickToContinue ctc;

    void Update()
    {
        if(clickToContinue)
        {
            if(ctc == null)
            {
                if (anyKey)
                    ctc = new ClickToContinue(gameScene);
                else
                    ctc = new ClickToContinue(gameScene, keyCode);
            }

            ctc.CheckInput();
        }
    }
}
