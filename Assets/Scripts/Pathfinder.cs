using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static Pathfinder Instance;
    
    // private List<Node> nodes;
    [SerializeField] private Node _startNode;
    [SerializeField] private Node _endNode;
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
        FindShortestPath(_startNode, _endNode);
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
                if (openSet[i].FCost < currentNode.FCost ||
                    openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return; // WIN!
            }

            foreach (Node neighbour in currentNode.GetNeighbours())
            {
                if (closedSet.Contains(neighbour))
                    continue;

                float newMovementCostToNeighbour = currentNode.gCost + GetNodeDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetNodeDistance(neighbour, targetNode);
                    neighbour.parentNode = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
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

    private float GetNodeDistance(Node source, Node target)
    {
        return Vector2.Distance(source.transform.position, target.transform.position);
    }

    public void SetTargetNode(Node targetNode)
    {
        _endNode = targetNode;
    }
    
    private void OnDrawGizmos()
    {
        if (_path == null || _path.Count <= 1)
            return;
        
        Color lineColor = new Color(0f, 1f, 1f);
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(_startNode.transform.position, _path[0].transform.position);
        for (int i = 0; i < _path.Count-1; i++)
        {
            Gizmos.DrawLine(_path[i].transform.position, _path[i+1].transform.position);
        }
    }
}