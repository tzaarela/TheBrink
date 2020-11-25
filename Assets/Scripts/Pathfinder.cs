using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static Pathfinder Instance;
    
    [SerializeField] private Node _startNode;
    [SerializeField] private Node _targetNode;
    [SerializeField] private List<Node> _path;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    
    private void Update()
    {
        FindShortestPath(_startNode, _targetNode);
    }

    private void FindShortestPath(Node startNode, Node targetNode)
    {
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                    currentNode = openSet[i];
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in currentNode.GetNeighbours())
            {
                if (closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetNodeDistance(currentNode, neighbour);
                
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetNodeDistance(neighbour, targetNode);
                    neighbour.parentNode = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    private void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();

        _path = path;
    }

    private int GetNodeDistance(Node source, Node target)
    {
        return (int)Vector2.Distance(source.Position, target.Position);
    }

    public void SetTargetNode(Node targetNode)
    {
        _targetNode = targetNode;
    }
    
    private void OnDrawGizmos()
    {
        if (_path == null || _path.Count <= 1)
            return;
        
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(_startNode.transform.position, _path[0].transform.position);
        for (int i = 0; i < _path.Count-1; i++)
        {
            Gizmos.DrawLine(_path[i].transform.position, _path[i+1].transform.position);
        }
    }
}