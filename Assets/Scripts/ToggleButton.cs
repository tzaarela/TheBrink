using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField]
    GameObject buttonToggleOn;

    [SerializeField]
    GameObject buttonToggleOff;

    public Action<bool> onToggle;
    
    public void Toggle(Toggle toggle)
    {
        buttonToggleOn.SetActive(!toggle.isOn);
        buttonToggleOff.SetActive(toggle.isOn);

        onToggle.Invoke(toggle.isOn);
    }
}
