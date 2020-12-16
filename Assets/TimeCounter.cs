using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeCounter : MonoBehaviour
{
    TextMeshProUGUI counter;
    DateTime dateTime;

    void Start()
    {
        dateTime = new DateTime(2298, 05, 12, 0,0,0);
        counter = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        dateTime = dateTime.AddSeconds(Time.deltaTime);
        counter.text = dateTime.ToString("yyyy/mm/dd - hh:mm:ss");
    }
}
