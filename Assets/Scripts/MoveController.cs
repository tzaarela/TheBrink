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

    private CrewMember crewMember;

    public float Speed { get => _speed; set => _speed = value; }

    public void Move()
    {
        // Debug.Log($"{name} - [MoveController] - Move");
        if (_path == null || _path.Count <= 0)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _path[0].Position, _speed * Time.deltaTime);
        
        if (WayPointIsReached())
        {
            crewMember.CurrentWayPoint = _path[0];
            _path.RemoveAt(0);
            if (_path.Count <= 0)
                FinishTask();
        }   
        
        DrawPath();
    }



    public void SetCrewMember(CrewMember crewMember)
    {
        this.crewMember = crewMember; 
    }

    private void FinishTask()
    {
        crewMember.TaskIsFinished(TaskType.Move);
    }

    private bool WayPointIsReached()
    {
        if (Vector2.Distance(transform.position, _path[0].Position) <= 0.1f)
            return true;
        return false;
    }
    
    public void FindShortestPath(Waypoint startWaypoint, Waypoint targetWaypoint)
    {
        List<Waypoint> openSet = new List<Waypoint>();
        List<Waypoint> closedSet = new List<Waypoint>();
        
        openSet.Add(startWaypoint);

        while (openSet.Count > 0)
        {
            Waypoint currentWaypoint = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentWaypoint.FCost ||
                    openSet[i].FCost == currentWaypoint.FCost && openSet[i].hCost < currentWaypoint.hCost)
                    currentWaypoint = openSet[i];
            }

            openSet.Remove(currentWaypoint);
            closedSet.Add(currentWaypoint);

            if (currentWaypoint == targetWaypoint)
            {
                RetracePath(startWaypoint, targetWaypoint);
                return;
            }

            foreach (Waypoint neighbour in currentWaypoint.GetNeighbours())
            {
                if (closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentWaypoint.gCost + GetNodeDistance(currentWaypoint, neighbour);
                
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetNodeDistance(neighbour, targetWaypoint);
                    neighbour.parentNode = currentWaypoint;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    private void RetracePath(Waypoint startWaypoint, Waypoint endWaypoint)
    {
        List<Waypoint> path = new List<Waypoint>();
        Waypoint currentWaypoint = endWaypoint;

        while (currentWaypoint != startWaypoint)
        {
            path.Add(currentWaypoint);
            currentWaypoint = currentWaypoint.parentNode;
        }
        path.Reverse();

        _path = path;
    }

    private int GetNodeDistance(Waypoint source, Waypoint target)
    {
        return (int)Vector2.Distance(source.Position, target.Position);
    }

    private void DrawPath()
    {
        if (_path == null || _path.Count <= 0)
            return;
        
        Debug.DrawLine(transform.position, _path[0].Position, Color.red);
        for (int i = 0; i < _path.Count - 1; i++)
        {
            Debug.DrawLine(_path[i].Position, _path[i+1].Position, Color.blue);
        }
    }
}
