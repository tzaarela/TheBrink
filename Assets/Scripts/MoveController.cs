using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // private List<Transform> _wayPoints;
    // [SerializeField]
    // private int _currentWayPoint;

    [SerializeField] private List<Waypoint> _path;

    [SerializeField]
    private Vector2 _velocity;

    [SerializeField] private float _speed;
    public float Speed { get => _speed; set => _speed = value; }

    public void Move()
    {
        Debug.Log($"{name} - [MoveController] - Move");
        if (_path == null || _path.Count <= 0)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _path[0].Position, _speed * Time.deltaTime);
    }

    private bool WayPointIsReached()
    {
        return false;
    }
}
