﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Tweening.Animations
{
    [Serializable]
    public class TextPrint : MonoBehaviour
    {
        public float timeInSeconds;
        public float delayInSeconds;

        private Text uiText;
        private string endValue;

        public void Awake()
        {
            uiText = GetComponent<Text>();
            endValue = uiText.text;
            uiText.text = "";
        }

        public float CompletionTime
        {
            get { return timeInSeconds + delayInSeconds; }
        }

        public Tween ExecuteTween()
        {
            return uiText.DOText(endValue, timeInSeconds, false).SetDelay(delayInSeconds);
        }
    }
}
