using Assets.Scripts.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Crew
{
    public class FixHullBreachCommand : Command
    {
        public bool IsExecuted { get; set; }
        public override string Name { get; set; }
        public override Room Destination { get; set; }
        public override bool IsFinished { get; set; }
        public override string StatusText { get; set; }

        private CrewMember crewMember;
        private ProgressBar progressBar;

        public FixHullBreachCommand(CrewMember crewMember, Room destination)
        {
            this.crewMember = crewMember;
            Destination = destination;
            Name = "Repair Hullbreach";
            StatusText = "Working...";
        }

        public override void Execute()
        {
            if (!IsExecuted)
            {
                progressBar = GameObject.Instantiate(CrewController.Instance.progressBarPrefab, crewMember.progressBarSlot.transform);
                IsExecuted = true;
            }

            crewMember.FixHullBreach();

            var fire = crewMember.CurrentCommand.Destination.Hazards.FirstOrDefault(x => x.HazardType == HazardType.Breach);

            if (fire == null)
            {
                GameObject.Destroy(progressBar.gameObject);
                IsFinished = true;
                return;
            }

            progressBar.SetProgressDone(fire.SeverityAmount);
        }
    }
}
