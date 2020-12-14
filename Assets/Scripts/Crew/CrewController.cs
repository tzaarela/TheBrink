using Assets.Scripts;
using Assets.Scripts.Crew;
using Assets.Scripts.Utility;
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
        if (Instance == null)
        {
            Instance = this;
            crewMembers = new List<CrewMember>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //Updates all commands but Move, crewMember.Update() handles this 
    public void UpdateCrewCommands()
    {
        foreach (var crewMember in crewMembers)
        {
            var commandQueue = crewMember.CommandQueue;
            if(commandQueue.Count() > 0 && commandQueue.Peek().GetType() != typeof(MoveCommand))
            {
                if (commandQueue.Peek().IsFinished)
                {
                    commandQueue.Dequeue();
                    if(commandQueue.Count > 0)
                        crewMember.CurrentCommand = commandQueue.Peek();
                }

                if (commandQueue.Count > 0)
                {
                    crewMember.Status = commandQueue.Peek().StatusText;
                    commandQueue.Peek().Execute();
                }
                else
                    crewMember.CurrentCommand = null;
            }
        }
    }

    public void CreateShipCrew()
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
