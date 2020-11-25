using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrewController : MonoBehaviour
{
    [SerializeField]
    private CrewMember[] crewMembersPrefab = new CrewMember[3];
    
    private static CrewController _instance;
    public static CrewController Instance { get { return _instance; } }

    public List<CrewMember> crewMembers; 
    
    

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
        crewMembers = new List<CrewMember>();
        AddCrew();
    }

    public void Update()
    {
        foreach (var crewMember in crewMembers)
        {
            if (crewMember.CurrentTask == null)
                continue;

            switch (crewMember.CurrentTask.TaskType)
            {
                case TaskType.Move:
                {
                    Debug.Log(crewMember.Name + " is moving...");
                        crewMember.Move();
                    break;
                }
                case TaskType.Repair:
                {
                    Debug.Log(crewMember.Name + " is repairing...");
                    break;
                }
                case TaskType.Investigate:
                {
                    Debug.Log(crewMember.Name + " is investigating...");
                    break;
                }

                case TaskType.None:
                    break;
                default:
                    break;
            }


        }
    }

    private void AddCrew()
    {
        Debug.Log("Adding crew...");
        foreach (CrewMember crewMember in crewMembersPrefab)
        {
            crewMembers.Add(crewMember);
        }
    }

    public CrewMember GetSelectedCrewMember()
    {
        return crewMembers.FirstOrDefault(x => x.IsSelected);
    }
}
