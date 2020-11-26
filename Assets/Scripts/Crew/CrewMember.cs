using System;
using Assets.Scripts;
using Assets.Scripts.InterfacePanels;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[System.Serializable]
public class CrewMember : UITrigger
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private float _health;
    [SerializeField]
    private Profession _profession;
    [SerializeField]
    private Waypoint _spawnPoint;
    [SerializeField]
    private float _moveSpeed = 6f;
    [SerializeField]
    private Transform highlight;
    public string Name { get => _name; set => _name = value; }
    public float Health { get => _health; set => _health = value; }
    public Profession Profession { get => _profession; set => _profession = value; }
    public Task CurrentTask { get; set; }
    public Waypoint CurrentWayPoint { get; set; }
    public bool IsMoving { get; set; }
    public bool IsSelected { 
        get { return isSelected; } 
        set 
        {
            isSelected = value;
            if (isSelected)
                ToggleHighlight(true);
            else
                ToggleHighlight(false);
        } 
    }

    private List<Waypoint> route = new List<Waypoint>();
    private int routeCount = 0;
    
    private MoveController _moveController;
    private bool isSelected;

    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
    }

    public void Start()
    {
        CurrentWayPoint = _spawnPoint;
        transform.position = _spawnPoint.transform.position;
    }

    public void ToggleHighlight(bool isOn)
    {
        Debug.Log($"{_name} - Toggling lights");
        if (isOn)
            highlight.gameObject.SetActive(true);
        else
            highlight.gameObject.SetActive(false);
    }

    public void Move()
    {
        
    }

    // public void Move()
    // {
    //     if (!IsMoving)
    //     {
    //         GetBestRoute();
    //     }
    //
    //     IsMoving = true;
    //
    //     Debug.Log("Crew is moving...");
    //
    //     transform.position = Vector2.MoveTowards(
    //     transform.position, route[routeCount].Position, _moveSpeed);
    //
    //     if ((Vector2)transform.position == route[routeCount].Position && routeCount < route.Count - 1)
    //     {
    //         CurrentWayPoint = route[routeCount];
    //         routeCount++;
    //     }
    //
    //     if ((Vector2)transform.position == CurrentTask.WayPoint.Position)
    //     {
    //         TaskController.instance.tasks.Remove(CurrentTask);
    //         IsMoving = false;
    //         CurrentWayPoint = CurrentTask.WayPoint;
    //         CurrentTask = null;
    //     }
    // }

    // private List<WayPoint> GetBestRoute()
    // {
    //     FindTarget(CurrentTask.WayPoint, CurrentWayPoint);
    //
    //     return null;
    // }

    // private WayPoint FindTarget(WayPoint target, WayPoint current)
    // {
    //     Debug.Log($"checking neighbour at position: x: {current.Position.x} y: {current.Position.y}");
    //     if (current == target)
    //     {
    //         Debug.Log("Found target");
    //         return target;
    //     }
    //
    //     if (current.Neighbours.Count == 0)
    //     {
    //         return null;
    //     }
    //
    //     foreach (var nextNeighbour in current.Neighbours)
    //     {
    //         route.Add(nextNeighbour);
    //         WayPoint found = FindTarget(target, nextNeighbour);
    //     }
    //
    //     return null;
    // }
}
