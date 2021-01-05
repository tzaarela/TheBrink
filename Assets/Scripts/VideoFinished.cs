using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoFinished : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;


    void Start()
    {
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;    
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        GameController.Instance.GameScene = Assets.Scripts.GameScene.SpaceportNoIntro;
    }
}
