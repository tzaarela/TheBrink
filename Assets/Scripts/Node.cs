using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private List<Node> _neighbours;
    
    public Vector2 Position { get; private set; }

    public int gCost;
    public int hCost;
    public int FCost { get { return gCost + hCost; } }

    public Node parentNode;

    private void Awake()
    {
        Position = transform.position;
    }

    public List<Node> GetNeighbours()
    {
        return _neighbours;
    }

    // private void OnMouseOver()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Pathfinder.Instance.SetTargetNode(this);
    //     }
    // }
}
