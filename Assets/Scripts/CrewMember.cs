using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    public Profession Profession { get; set; }
    public string Name { get; set; }
    public float Health { get; set; }


    private CrewController _crewController;

    private void Awake()
    {
        _crewController = new CrewController();
    }
        
    private void Move()
    {
        
    }
}
