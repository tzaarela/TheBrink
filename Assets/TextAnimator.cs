using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class TextAnimator : MonoBehaviour
{
    public float speedMultiplier;
    public float delayInSeconds;

    int sequenceCounter = 0;
    List<Text> uiTexts;
    List<string> endValues;

    void Start()
    {
        uiTexts = GetComponentsInChildren<Text>().ToList();
        endValues = uiTexts.Select(x => x.text).ToList();
        uiTexts.ForEach(x => x.text = "");

        PlayNext();
    }

    public void PlayNext()
    {
        if(sequenceCounter < uiTexts.Count)
        {
            var uiText = uiTexts[sequenceCounter];
            var endValue = endValues[sequenceCounter];
            uiText.DOText(endValue, endValue.Length / speedMultiplier).SetDelay(delayInSeconds).onComplete += PlayNext;
            sequenceCounter++;
        }
    }
}
