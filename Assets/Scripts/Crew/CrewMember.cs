using Assets.Scripts.Crew;
using Assets.Scripts.InterfacePanels;
using Assets.Scripts.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class CrewMember : UITrigger
{
    [SerializeField]
    private Waypoint _spawnPoint;
    [SerializeField]
    private Transform highlight;
    [SerializeField]
    private float speed;

    public CrewData crewData;
    public string Status { get; set; }
    public Command CurrentCommand { get; set; }
    public Queue<Command> CommandQueue { get; set; }
    public Waypoint CurrentWayPoint { get; set; }
    public bool IsMoving { get; set; }
    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            if (isSelected)
            {
                ToggleHighlight(true);
            }

            else
                ToggleHighlight(false);
        }

    }

    public MoveController moveController;
    private bool isSelected;

    private void Awake()
    {
        Status = "Idle";
        moveController = new MoveController(speed, this);
        CommandQueue = new Queue<Command>();
        CurrentWayPoint = _spawnPoint;
        transform.position = _spawnPoint.transform.position;
    }

    public void Start()
    {
    }

    public void Update()
    {
        if (CommandQueue.Count > 0 && CommandQueue.Peek().GetType() == typeof(MoveCommand))
        {
            if (CommandQueue.Peek().IsFinished)
            {
                CommandQueue.Dequeue();
                if (CommandQueue.Count > 0)
                    this.CurrentCommand = CommandQueue.Peek();
            }

            if (CommandQueue.Count > 0)
            {
                Status = CommandQueue.Peek().StatusText;
                CommandQueue.Peek().Execute();
            }
        }
        else if (CommandQueue.Count == 0)
            Status = "Idle";
    }

    public void Select()
    {
        CrewController.Instance.crewMembers.ForEach(x => x.IsSelected = false);
        IsSelected = true;
    }

    public void ToggleHighlight(bool isOn)
    {
        if (isOn)
            highlight.gameObject.SetActive(true);
        else
            highlight.gameObject.SetActive(false);
    }

    public void AddCommand(Command command)
    {
        //If we are not there, make sure to add a move command first.
        if (command.GetType() != typeof(MoveCommand) && CurrentWayPoint != command.Destination.waypoints[0])
            CommandQueue.Enqueue(new MoveCommand(this, command.Destination));

        CommandQueue.Enqueue(command);
    }

    public void Move()
    {
        moveController.Move();
    }

    public void Repair()
    {
        CurrentCommand.Destination.RepairRoom(this);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }
}
