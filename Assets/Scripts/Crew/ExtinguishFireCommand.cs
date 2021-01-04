using Assets.Scripts;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishFireCommand : Command
{
    public bool IsExecuted { get; set; }
    public override string Name { get; set; }
    public override Room Destination { get; set; }
    public override bool IsFinished { get; set; }
    public override string StatusText { get; set; }

    private CrewMember crewMember;

    public ExtinguishFireCommand(CrewMember crewMember, Room destination)
    {
        this.crewMember = crewMember;
        Destination = destination;
        Name = "Extinguish Fire";
        StatusText = "Extinguishing fire...";
    }

    public override void Execute()
    {
        crewMember.ExtinguishFire();

        if (crewMember.CurrentCommand.Destination.Hazards.Count <= 0)
            IsFinished = true;
    }
}
