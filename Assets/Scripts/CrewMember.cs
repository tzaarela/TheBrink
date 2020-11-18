using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    public Profession Profession { get; set; }
    public string Name { get; set; }
    public float Health { get; set; }


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
