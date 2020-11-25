﻿using Assets.Scripts;
using Assets.Scripts.InterfacePanels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Room : UITrigger
{
    public float AirLevel { get; set; }
    public float RadiationLevel { get; set; }
    public bool HasElectricity { get; set; }
    //Added this trait here, think we are going to need it, hope it's okay? - DJ
    public float RoomHealth { get; set; }
    public RoomType RoomType { get; set; }

    private bool isSelected;

    public bool IsSelected {
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
    private WayPoint wayPoint;

    [SerializeField]
    private RoomType _roomType;

    public void Start()
    {
        AirLevel = 100;
        RadiationLevel = 0;
        RoomHealth = 100;
        HasElectricity = true;
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

    public void CreateHazard(HazardType hazardType, float severityAmount)
    {
        Hazard hazard = new Hazard(hazardType, severityAmount);

        Hazards.Add(hazard);
    }

    public List<Task> GetAvailableTasks()
    {
        List<Task> availableTasks = new List<Task>();
        availableTasks.Add(new Task(TaskType.Move, wayPoint));
        availableTasks.Add(new Task(TaskType.Investigate, wayPoint));

        if(Hazards.Count > 0)
        {
            availableTasks.Add(new Task(TaskType.Repair, wayPoint));
        }


        return availableTasks;
        
    }

    public void RepairRoom(HazardType _hazardTypeToRepair)
    {
        //TODO: Talk to JS or EN about this
                //Suggestion 1: Will this float be seen in Unity for ease of manipulation? To figure if repair goes to slowly?
                //Suggestion 2: Could this be turned into a property of each crewmember later on maybe?
        float _crewMemberRepairSkill = 5;

        foreach(Hazard Hazard in Hazards)
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
        /*
         * Okay, so, to fix a room.
         * And I need a way to find out the damages on the room.
         * So, suppose that we put a function in crewmembers, that lets them repair a room.
         * As of now, the rooms are actually not damaged, severityAmount is it's own thing.
         * So the only way to repair now is to decrease SeverityAmount.
         * So the Room will need to find the hazard, and then change that hazards SeverityAmount...
         * So it will need to do this once for every hazard?
         * It will need to go through its own list?
         * 
         * So, first, check if there is a crewmember in the room...
         * if it is, send in their "skill" value in repair.
         * And maybe what type of hazard they want to work against?
         * And then in repair, it checks for that hazard, and then lets them fight it?
         * (decreases its severityAmount for now)
         * So using this method will look like Room.RepairRoom(hazardType _hazardToRepair) ?
        */
    }


    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SelectRoom();
            ContextMenuController.instance.CloseContextMenu();
        }
        else if(Input.GetMouseButtonDown(1))
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
