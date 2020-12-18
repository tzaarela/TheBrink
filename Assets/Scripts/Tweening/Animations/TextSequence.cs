using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Tweening.Animations;
using System;
using DG.Tweening;

public class TextSequence : MonoBehaviour 
{
    public List<TextPrint> textPrints;
    public InputTextHandler inputTextHandler;
    public Action onComplete;
    public bool isExecuted;

    private Sequence sequence;

    private void Awake()
    {
        sequence = DOTween.Sequence();
        if (inputTextHandler != null)
            sequence.onComplete += () => ExecuteInput();
        else
            sequence.onComplete += () => onComplete.Invoke();
    }

    public void AddTweens(Sequence sequence)
    {
        textPrints.ForEach(x => sequence.Append(x.ExecuteTween()));
        sequence.Play();
    }

    public void ExecuteSequence()
    {
        gameObject.SetActive(true);
        isExecuted = true;
        AddTweens(sequence);
    }

    public void ExecuteInput()
    {
        inputTextHandler.ActivateInputField();
        StartCoroutine(WaitForInput());
    }

    IEnumerator WaitForInput()
    {
        yield return new WaitUntil(isEnterPressed);

        GameController.Instance.ship.captainName = inputTextHandler.input.text;
        onComplete.Invoke();
    }

    public bool isEnterPressed()
    {
        return Input.GetKeyDown(KeyCode.Return);
    }
}

