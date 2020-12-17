using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Tweening.Animations
{
    [Serializable]
    public class TextSequence : MonoBehaviour, ITweenObject
    {
        public float timeInSeconds;
        public float delayInSeconds;

        private Text uiText;
        private string endValue;
        private float timeUntilNextTween;

        public void Start()
        {
            uiText = GetComponent<Text>();
            endValue = uiText.text;
            uiText.text = "";
        }

        public float CompletionTime
        {
            get { return timeInSeconds + delayInSeconds; }
        }

        public float TimeUntilNextTween
        {
            get { return timeUntilNextTween; }
        }

        public void ExecuteTween()
        {
            uiText.DOText(endValue, timeInSeconds).SetDelay(delayInSeconds);
        }
    }
}
