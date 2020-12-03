using Assets.Scripts.InterfacePanels;
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
    private Transform warningHighlight;
    [SerializeField]
    private Waypoint _waypoint;
    public Waypoint Waypoint
    {
        get { return _waypoint; }
    }
    
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
        Hazard hazard = new Hazard(_hazardType, _severityAmount, this);

        Hazards.Add(hazard);
    }

    public void UpdateHazard()
    {
        Hazards.RemoveAll(x => x.IsFinished);

        if(Hazards.Count > 0)
            warningHighlight.gameObject.SetActive(true);
        else
            warningHighlight.gameObject.SetActive(false);

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
        availableTasks.Add(new Task(TaskType.Scan, this));

        if (Hazards.Count > 0)
        {
            availableTasks.Add(new Task(TaskType.Repair, this));
        }

        return availableTasks;
    }

    public void RepairRoom()
    {
        foreach (Hazard Hazard in Hazards)
        {
                
                //TODO: Ask Saarela about easiest way to do this, should this method be moved to CrewMember instead?
                //Hazard.SeverityAmount -= _crewMemberRepairSkill;
                
                //TODO: Figure out upper bounds of this, can a fire burn too fast?

                //I think this will mean that the crewmember will do one repair action, and then get sent out of the method.
                
                return;
        }

        Debug.Log("The crewmember couldn't find a hazard of the type that they were told to repair");
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !ContextMenuController.instance.IsOpen)
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
        Debug.Log($"Room \"{name}\" selected");

        var previouslySelected = RoomController.Instance.Rooms.FirstOrDefault(x => x.IsSelected == true);
        if (previouslySelected != null)
            previouslySelected.IsSelected = false;

        IsSelected = true;
    }
}
