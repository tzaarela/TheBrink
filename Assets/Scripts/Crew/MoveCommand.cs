using Assets.Scripts;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public CrewMember commandOwner;
    public bool IsExecuted { get; set; }
    public override string Name { get; set; }
    public override Room Destination { get; set; }
    public override bool IsFinished { get; set; }
    public override string StatusText { get; set; }

    public MoveCommand(CrewMember commandOwner, Room destination)
    {
        this.Destination = destination;
        this.commandOwner = commandOwner;
        this.Name = "Move";
        this.StatusText = "Moving";
    }

    public override void Execute()
    {
        if (!IsExecuted)
        {
            commandOwner.moveController.FindShortestPath(commandOwner.CurrentWayPoint, Destination.waypoints[0]);
            IsExecuted = true;
        }

        commandOwner.moveController.Move();

        if (commandOwner.CurrentWayPoint == Destination.waypoints[0]) 
            IsFinished = true;
    }

    
}
