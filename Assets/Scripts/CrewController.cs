using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : MonoBehaviour
{
    [SerializeField]
    private WayPoint spawnPoint;

    [SerializeField]
    private float moveSpeed = 1f;

    public List<CrewMember> crewMembers = new List<CrewMember>();
    public GameObject crewPrefab;
    private static CrewController _instance;

    public static CrewController Instance { get { return _instance; } }

    private List<WayPoint> route = new List<WayPoint>();
    private int routeCount = 0;
    

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        CreateCrew();
    }

    public void Update()
    {
        foreach (var crewMember in crewMembers)
        {
            if (crewMember.CurrentTask != null && crewMember.CurrentTask.TaskType == TaskType.Move)
            {
                Move(crewMember);
            }
        }
    }

    private void CreateCrew()
    {
        Debug.Log("Creating crew...");
        CrewMember crewMember = new CrewMember("John Doe", 100, Profession.Engineer, spawnPoint);
        crewMembers.Add(crewMember);
        crewMember.CrewObject =  Instantiate(crewPrefab, spawnPoint.Position, Quaternion.identity);
    }

    private void Move(CrewMember crewMember)
    {
        if(!crewMember.IsMoving)
        {
            GetBestRoute(crewMember);
        }

        crewMember.IsMoving = true;

        Debug.Log("Crew is moving...");

        crewMember.CrewObject.transform.position = Vector2.MoveTowards(
            crewMember.CrewObject.transform.position, route[routeCount].Position, moveSpeed);

        if((Vector2)crewMember.CrewObject.transform.position == route[routeCount].Position && routeCount < route.Count - 1)
        {
            crewMember.CurrentWayPoint = route[routeCount];
            routeCount++;
        }

        if((Vector2)crewMember.CrewObject.transform.position == crewMember.CurrentTask.WayPoint.Position)
        {
            TaskController.instance.tasks.Remove(crewMember.CurrentTask);
            crewMember.IsMoving = false;
            crewMember.CurrentWayPoint = crewMember.CurrentTask.WayPoint;
            crewMember.CurrentTask = null;
        }
    }

    private List<WayPoint> GetBestRoute(CrewMember crewMember)
    {
        FindTarget(crewMember.CurrentTask.WayPoint, crewMember.CurrentWayPoint);

        return null;
    }

    private WayPoint FindTarget(WayPoint target, WayPoint current)
    {
        Debug.Log($"checking neighbour at position: x: {current.Position.x} y: {current.Position.y}");
        if(current == target)
        {
            Debug.Log("Found target");
            return target;
        }

        if (current.Neighbours.Count == 0)
        {
            return null;
        }

        foreach (var nextNeighbour in current.Neighbours)
        {
            route.Add(nextNeighbour);
            WayPoint found = FindTarget(target, nextNeighbour);
        }

        return null;
    }


}
