using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : MonoBehaviour
{
    [SerializeField]
    private WayPoint spawnPoint1;

    [SerializeField]
    private WayPoint spawnPoint2;

    [SerializeField]
    private WayPoint spawnPoint3;

    [SerializeField]
    private float moveSpeed = 1f;

    public GameObject crewPrefab;
    private static CrewController _instance;

    public static CrewController Instance { get { return _instance; } }

    public  List<CrewMember> crewMembers = new List<CrewMember>();
    private List<WayPoint> path = new List<WayPoint>();
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
        CrewMember crewMember1 = new CrewMember("John Doe", 100, Profession.Engineer, spawnPoint1);
        CrewMember crewMember2 = new CrewMember("Jane Doe", 100, Profession.Scientist, spawnPoint2);
        CrewMember crewMember3 = new CrewMember("Jimmy Doe", 100, Profession.Technician, spawnPoint3);

        crewMembers.Add(crewMember1);
        crewMembers.Add(crewMember2);
        crewMembers.Add(crewMember3);

        crewMember1.CrewObject =  Instantiate(crewPrefab, spawnPoint1.Position, Quaternion.identity);
        crewMember2.CrewObject = Instantiate(crewPrefab, spawnPoint2.Position, Quaternion.identity);
        crewMember3.CrewObject = Instantiate(crewPrefab, spawnPoint3.Position, Quaternion.identity);

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
            crewMember.CrewObject.transform.position, path[routeCount].Position, moveSpeed);

        if((Vector2)crewMember.CrewObject.transform.position == path[routeCount].Position && routeCount < path.Count - 1)
        {
            crewMember.CurrentWayPoint = path[routeCount];
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
            path.Add(nextNeighbour);
            WayPoint found = FindTarget(target, nextNeighbour);
        }

        return null;
    }


}
