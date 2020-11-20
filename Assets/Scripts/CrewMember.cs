using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CrewMember
{
    public string Name { get; set; }
    public float Health { get; set; }
    public Profession Profession { get; set; }
    public Task CurrentTask { get; set; }
    public WayPoint CurrentWayPoint { get; set; }
    public GameObject CrewObject { get; set; }
    public bool IsMoving { get; set; }


    private float moveSpeed = 2f;

    private MoveController _moveController;

    public CrewMember(string name, float health, Profession profession, WayPoint currentWayPoint)
    {
        Name = name;
        Health = health;
        Profession = profession;
        CurrentWayPoint = currentWayPoint;
    }
}
