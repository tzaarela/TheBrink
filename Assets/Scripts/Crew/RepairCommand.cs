using Assets.Scripts;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairCommand : Command
{
    public bool IsExecuted { get; set; }
    public override string Name { get; set; }
    public override Room Destination { get; set; }
    public override bool IsFinished { get; set; }
    public override string StatusText { get; set; }

    private CrewMember crewMember;

    public RepairCommand(CrewMember crewMember, Room destination)
    {
        this.crewMember = crewMember;
        Destination = destination;
        Name = "Repair";
        StatusText = "Repairing";
    }

    public override void Execute()
    {
        crewMember.Repair();

        if (crewMember.CurrentCommand.Destination.Hazards.Count <= 0)
            IsFinished = true;
    }
}
