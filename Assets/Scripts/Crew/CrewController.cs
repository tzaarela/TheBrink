using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrewController : MonoBehaviour
{
    public static CrewController Instance;
    
    [SerializeField]
    private CrewMember[] crewMembersPrefab = new CrewMember[3];

    public List<CrewMember> crewMembers;

    [SerializeField]
    private CrewMember _currentCrewMember;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
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

    public void SetCurrentCrewMember(int index)
    {
        _currentCrewMember = crewMembers[index];
    }
}
