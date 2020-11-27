﻿using Assets.Scripts.InterfacePanels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Room : UITrigger
{

    public float AirLevel { get => airLevel; set => airLevel = value; }
    public float RadiationLevel { get => radiationLevel; set => radiationLevel = value; }
    public float RoomHealth { get => roomHealth; set => roomHealth = value; }
    public bool HasElectricity { get => hasElectricity; set => hasElectricity = value; }
    public RoomType RoomType { get; set; }

    private bool isSelected;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            if (isSelected)
                onRoomSelected.Invoke();
            else
                onRoomDeselected.Invoke();

        }
    }
    public List<Hazard> Hazards { get; set; }

    private Door _door;
    private UnityEvent onRoomSelected;

    private UnityEvent onRoomDeselected;

    [SerializeField]
    private Transform highlight;
    [SerializeField]
    private Waypoint _waypoint;
    [SerializeField]
    private RoomType _roomType;
    [SerializeField]
    private float airLevel;
    [SerializeField]
    private float radiationLevel;
    [SerializeField]
    private float roomHealth;
    [SerializeField]
    private bool hasElectricity;

    public void Start()
    {
        Hazards = new List<Hazard>();
        RoomType = _roomType;

        if (onRoomSelected == null)
            onRoomSelected = new UnityEvent();

        if (onRoomDeselected == null)
            onRoomDeselected = new UnityEvent();

        onRoomSelected.AddListener(OnRoomSelected);
        onRoomDeselected.AddListener(OnRoomDeselected);
    }

    private void OnRoomSelected()
    {
        highlight.gameObject.SetActive(true);
    }

    private void OnRoomDeselected()
    {
        highlight.gameObject.SetActive(false);
    }

    public void CreateHazard(HazardType _hazardType, float _severityAmount)
    {
        Hazard hazard = new Hazard(_hazardType, _severityAmount);

        Hazards.Add(hazard);
    }

    public void UpdateHazard()
    {
        Hazards.RemoveAll(x => x.IsFinished);

        foreach (Hazard hazard in Hazards)
        {
            hazard.ExecuteHazard();
        }
    }

    public List<Task> GetAvailableTasks()
    {
        //TODO - GET CORRECT TASKS FOR ROOM AND CHARACTER
        List<Task> availableTasks = new List<Task>();
        availableTasks.Add(new Task(TaskType.Move, this));
        availableTasks.Add(new Task(TaskType.Investigate, this));

        if (Hazards.Count > 0)
        {
            availableTasks.Add(new Task(TaskType.Repair, this));
        }


        return availableTasks;

    }

    public void RepairRoom(HazardType _hazardTypeToRepair)
    {
        //TODO: Talk to JS or EN about this
        //Suggestion 2: Could this be turned into a property of each crewmember later on maybe?
        float _crewMemberRepairSkill = 5;

        foreach (Hazard Hazard in Hazards)
        {
            if (Hazard.HazardType == _hazardTypeToRepair)
            {
                Hazard.SeverityAmount -= _crewMemberRepairSkill;
                //TODO: Figure out upper bounds of this, can a fire burn too fast?

                //I think this will mean that the crewmember will do one repair action, and then get sent out of the method.
                return;
            }
        }

        Debug.Log("The crewmember couldn't find a hazard of the type they were told to repair");
    }

    private void OnMouseOver()
    {
        Debug.Log($"{name} OnMouseOver!");
        if (Input.GetMouseButtonDown(0))
        {
            SelectRoom();
            ContextMenuController.instance.CloseContextMenu();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SelectRoom();
            var availableTasks = GetAvailableTasks();
            ContextMenuController.instance.OpenContextMenu(availableTasks);
        }
    }
    public void SelectRoom()
    {
        Debug.Log("room selected");

        var previouslySelected = RoomController.Instance.Rooms.FirstOrDefault(x => x.IsSelected == true);
        if (previouslySelected != null)
            previouslySelected.IsSelected = false;

        IsSelected = true;
    }

}
