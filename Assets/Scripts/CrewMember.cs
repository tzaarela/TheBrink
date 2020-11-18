using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    private CrewController _crewController;

    private void Awake()
    {
        _crewController = GetComponent<CrewController>();
    }

    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        _crewController.Move();
    }
}
