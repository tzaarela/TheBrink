using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    private CrewController _crewController;

    private void Awake()
    {
        _crewController = new CrewController();
    }
    
    private void Move()
    {
        
    }
}
