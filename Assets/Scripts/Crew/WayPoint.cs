using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> _neighbours;
    
    public Vector2 Position { get; private set; }

    [HideInInspector]
    public int gCost;    // Distance cost between Start and Current Waypoint
    [HideInInspector]
    public int hCost;    // Estimated distance cost between Current and Target Waypoint
    public int FCost { get { return gCost + hCost; } }    // Totalt distance cost of the Waypoint

    public Waypoint parentNode;

    private void Awake()
    {
        Position = transform.position;
    }

    public List<Waypoint> GetNeighbours()
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
