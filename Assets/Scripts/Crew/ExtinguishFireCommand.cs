using Assets.Scripts;
using Assets.Scripts.Crew;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExtinguishFireCommand : Command
{
    public bool IsExecuted { get; set; }
    public override string Name { get; set; }
    public override Room Destination { get; set; }
    public override bool IsFinished { get; set; }
    public override string StatusText { get; set; }

    private CrewMember crewMember;
    private ProgressBar progressBar;

    public ExtinguishFireCommand(CrewMember crewMember, Room destination)
    {
        this.crewMember = crewMember;
        Destination = destination;
        Name = "Extinguish Fire";
        StatusText = "Working...";
    }

    public override void Execute()
    {
        if (!IsExecuted)
        {
            progressBar = GameObject.Instantiate(CrewController.Instance.progressBarPrefab, crewMember.progressBarSlot.transform);
            IsExecuted = true;
        }

        crewMember.ExtinguishFire();

        var fire = crewMember.CurrentCommand.Destination.Hazards.FirstOrDefault(x => x.HazardType == HazardType.Fire);

        if (fire == null)
        {
            GameObject.Destroy(progressBar.gameObject);
            IsFinished = true;
            return;
        }

        progressBar.SetProgressDone(fire.SeverityAmount);
    }
}
