using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoFinished : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    public GameScene swapToScene;


    void Start()
    {
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        GameController.Instance.GameScene = swapToScene;
    }
}
