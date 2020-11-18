using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private List<Transform> _wayPoints;
    [SerializeField]
    private int _currentWayPoint;

    [SerializeField]
    private Vector2 _velocity;

    public void Move()
    {
        
    }

    private bool WayPointIsReached()
    {
        return false;
    }
}
