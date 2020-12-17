using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTextHandler : MonoBehaviour
{
    InputField input;

    private void Start()
    {
        input = GetComponent<InputField>();
    }

    public void ToUpperCase()
    {
        input.text = input.text.ToUpper();
    }
}
