using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Tweening.Animations;
using System;

public class TextSequenceAnimator : MonoBehaviour
{
    public List<TextSequence> textSequences;
    TweenQueue tweenQueue;

    void Start()
    {
        tweenQueue = GetComponent<TweenQueue>();
        Play();
    }

    public void Play()
    {
        foreach (var sequence in textSequences)
        {
            tweenQueue.AddTweenToQueue(sequence);
        }
    }
}

