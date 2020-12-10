using Assets.Scripts;
using Assets.Scripts.InterfacePanels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Room : UITrigger
{
    //Remove all of these gets and sets?
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
    public List<Waypoint> waypoints;
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

        warningHighlight.gameObject.SetActive(Hazards.Count > 0);
        
        foreach (Hazard hazard in Hazards)
        {
            hazard.UpdateHazard();
        }
    }

    public void AirDrain(float drainLevel)
    {
        AirLevel -= drainLevel;
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

    public void RepairRoom(CrewMember crewMember)
    {
        Hazard hazardToFix;

        if (Hazards.Count <= 0)
        {
            ConsoleController.instance.PrintToConsole($"{crewMember.name}: I've finished the repairs in {crewMember.CurrentTask.Destination.name}. ", 0.01f, true);
            crewMember.FinishCurrentTask();
            return;
        }
        else
        {
            hazardToFix = Hazards[0];
        }

        foreach(Hazard hazard in Hazards)
        {
            if(hazard.HazardType == HazardType.Fire)
            {
                hazardToFix = hazard;
                break;
            }
        }

        if (hazardToFix.SeverityAmount <= 0)
        {
            hazardToFix.IsFinished = true;
        }
        else
        {
            switch (hazardToFix.HazardType)
            {
                case HazardType.Fire:
                    hazardToFix.SeverityAmount -= crewMember.RepairSkill;
                    Debug.Log("The crewmember is trying to put out the fire.");
                    break;
                    ///<summary>
                    ///Important to know here, is that the Breach only fixes the oxygen leakage.
                    ///It does not fix the actual damage to the roomHealth, which will need to be fixed in starport. 
                    ///</summary>
                case HazardType.Breach:
                    hazardToFix.SeverityAmount -= crewMember.RepairSkill;
                    Debug.Log("The crewmember is trying to patch over the hull breach.");
                    break;
                default:
                    crewMember.FinishCurrentTask();
                    Debug.Log("The crewmember couldn't find a hazard that they are able told to repair");
                    break;
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Highlight(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Highlight(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //if (Input.GetMouseButtonDown(0) && !ContextMenuController.instance.IsOpen)
        //{
        //    ContextMenuController.instance.CloseContextMenu();
        //}

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //RoomController.Instance.Rooms.FirstOrDefault(x => x.IsSelected == true).IsSelected = false;
            //IsSelected = true;

            if (CrewController.Instance.GetSelectedCrewMember() != null)
            {
                var availableTasks = GetAvailableTasks();
                ContextMenuController.instance.OpenContextMenu(availableTasks);
            }
        }
    }
    public void Highlight(bool isOn)
    {
        if(isOn)
            highlight.gameObject.SetActive(true);
        else
            highlight.gameObject.SetActive(false);
    }
}
