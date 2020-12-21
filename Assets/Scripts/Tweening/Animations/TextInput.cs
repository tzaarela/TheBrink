using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField input;

    private void Awake()
    {
        gameObject.SetActive(false);
        input = GetComponent<InputField>();
    }
    
    public void ToUpperCase()
    {
        input.text = input.text.ToUpper();
    }

    public void ActivateInputField()
    {
        gameObject.SetActive(true);
        input.Select();
    }
}
