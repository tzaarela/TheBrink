using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // public static Pathfinder Instance;
    
    [SerializeField] private List<Waypoint> _path;

    private void Awake()
    {
        // if(Instance == null)
        //     Instance = this;
        // else
        //     Destroy(this);
    }
    
    private void Update()
    {
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
}