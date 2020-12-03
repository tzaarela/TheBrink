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

    public void UpdateCrew()
    {
        if (crewMembers == null || crewMembers.Count <= 0)
            return;
        
        foreach (var crewMember in crewMembers)
        {
            if (crewMember == null || crewMember.CurrentTask == null)
                continue;

            var task = crewMember.CurrentTask;

           
            switch (task.TaskType)
            {
                case TaskType.Move:
                {       
                        Debug.Log(crewMember.Name + " is moving...");
                        crewMember.Move();
                    break;
                }
                case TaskType.Repair:
                {
                        if (!task.IsExecuted)
                        {
                            ConsoleController.instance.PrintToConsole($"{crewMember.Name}: I've started repairs in {task.Destination.name}", 0.04f);
                            task.IsExecuted = true;
                        }
                        crewMember.Repair();
                    break;
                }
                case TaskType.Scan:
                {
                        Debug.Log(crewMember.Name + "is scanning...");
                        ConsoleController.instance.PrintToConsole(
                            $"Room status [{task.Destination.RoomType.ToString()}]" +
                            "\n -- Oxygen level: " + task.Destination.AirLevel + "%" +
                            "\n -- Radiation level: " + task.Destination.RadiationLevel + "%" +
                            "\n -- Hull Integrity: " + task.Destination.RoomHealth + "% ", 0.04f);
                        crewMember.CurrentTask = null;
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
