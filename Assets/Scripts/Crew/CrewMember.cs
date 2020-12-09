using Assets.Scripts.InterfacePanels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private float repairSkill;
    [SerializeField]
    private Transform highlight;
    public string Name { get => _name; set => _name = value; }
    public float Health { get => _health; set => _health = value; }
    public Profession Profession { get => _profession; set => _profession = value; }
    public float RepairSkill { get => repairSkill; set => repairSkill = value; }
    public string Status { get; set; }
    public Task CurrentTask { get; set; }
    public Queue<Task> TaskQueue { get; set; }
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

    private MoveController _moveController;
    private bool isSelected;

    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
        _moveController.SetCrewMember(this);
        TaskQueue = new Queue<Task>();
    }

    public void Start()
    {
        Status = "Idle";
        CurrentWayPoint = _spawnPoint;
        transform.position = _spawnPoint.transform.position;
    }

    public void Update()
    {
        if (CurrentTask != null && CurrentTask.TaskType == TaskType.Move)
        {
            Status = "Moving";
            Move();
        }
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

    public void AddTask(Task newTask)
    {
        TaskQueue.Enqueue(newTask);
        CurrentTask = TaskQueue.Peek();

        if (CurrentTask.TaskType == TaskType.Move)
        {
            Debug.Log($"{_name} got a Move Task!");
            _moveController.FindShortestPath(CurrentWayPoint, CurrentTask.Destination.Waypoint);
        }
    }

    public void Move()
    {
        _moveController.Move();
    }

    public void Repair()
    {
        Status = "Repairing";
        CurrentTask.Destination.RepairRoom(this);
    }

    public void FinishCurrentTask()
    {
        TaskQueue.Dequeue();

        if (TaskQueue.Count > 0)
        {
            CurrentTask = TaskQueue.Peek();

            if (CurrentTask.TaskType == TaskType.Move)
            {
                _moveController.FindShortestPath(CurrentWayPoint, CurrentTask.Destination.Waypoint);
            }
        }
        else
        {
            CurrentTask = null;
            Status = "Idle";
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }
}
