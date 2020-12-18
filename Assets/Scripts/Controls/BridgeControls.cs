using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BridgeControls : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI etaValue;

    SystemController systemController;

    void Start()
    {
        systemController = SystemController.Instance;
    }

    void Update()
    {
        //etaValue.text = Convert.ToInt32(systemController.estimatedTimeToArrival).ToString();
    }
}
