using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewController
{
    private List<Transform> _wayPoints;
    [SerializeField] private int _currentWayPoint;
    public CrewController()
    {
        
    }

    public void Move(Vector2 velocity)
    {
        
    }

    private bool WayPointIsReached()
    {
        return false;
    }
}
