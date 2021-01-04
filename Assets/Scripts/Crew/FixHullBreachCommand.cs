using Assets.Scripts.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public FixHullBreachCommand(CrewMember crewMember, Room destination)
        {
            this.crewMember = crewMember;
            Destination = destination;
            Name = "Repair Hullbreach";
            StatusText = "Repairing hullbreach...";
        }

        public override void Execute()
        {
            crewMember.FixHullBreach();

            if (!crewMember.CurrentCommand.Destination.Hazards.Any(x => x.HazardType == HazardType.Breach))
                IsFinished = true;
        }
    }
}
