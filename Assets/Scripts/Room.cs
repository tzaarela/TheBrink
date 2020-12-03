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

    public void RepairRoom(float repairSkill)
    {
        /*
         * So, I need this method to set a priority for what hazard is fixed first right...?
         * So it will need to check if the Room has a fire right?
         * And later on you will need to change so that there can only ever be ONE fire in each room.
         * And if there is a fire it shouldn't try to solve anything else...
         * do-while? maybe? so...
         * if hazard.hazardType == hazardType.Fire then change enum? blah blah
         * else cont.?
         * then check enum and go after that?
         * 
         * imagine if we had a hazard called "hazard to treat".
         * and we let that hazard 
         * 
         * if we have a first method that checks for fire within the list...
         * 
         * and if not it does something else?
         * 
         * also, needs a way here to remove hazards if they are finished.
         * should it go through the list and do that all the time?
         * 
         * and something maybe that hurts crewMembers?
         * 
         * no this doesn't work at all.
         */

        Hazard _hazardToFix = Hazards.FirstOrDefault(x => x.HazardType == HazardType.Fire);

        switch (_hazardToFix.HazardType)
        {
            case HazardType.Fire:
                _hazardToFix.SeverityAmount -= repairSkill * 2;
                Debug.Log("The crewmember is trying to put out the fire.");
                break;
            default:
                Debug.Log("The crewmember couldn't find a hazard that they are able told to repair");
                break;
        }
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
