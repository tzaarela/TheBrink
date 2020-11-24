using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private List<Node> _neighbours;

    public float gCost;
    public float hCost;

    public Node parentNode;
    
    // [SerializeField]
    // private Color 

    public List<Node> GetNeighbours()
    {
        return _neighbours;
    }
    
    public float FCost
    {
        get { return gCost + hCost; }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Pathfinder.Instance.SetTargetNode(this);
        }
    }
}
