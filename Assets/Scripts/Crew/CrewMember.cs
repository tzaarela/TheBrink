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
        if (isOn)
            highlight.gameObject.SetActive(true);
        else
            highlight.gameObject.SetActive(false);
    }

    public void Move()
    {
        
    }
}
