using Assets.Scripts.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Crew
{
    public class FixElectricCommand : Command
    {
        public bool IsExecuted { get; set; }
        public override string Name { get; set; }
        public override Room Destination { get; set; }
        public override bool IsFinished { get; set; }
        public override string StatusText { get; set; }

        private CrewMember crewMember;

        public FixElectricCommand(CrewMember crewMember, Room destination)
        {
            this.crewMember = crewMember;
            Destination = destination;
            Name = "Fix Electronics";
            StatusText = "Fixing electronics...";
        }

        public override void Execute()
        {
            crewMember.FixElectronics();

            if (crewMember.CurrentCommand.Destination.Hazards.Count <= 0)
                IsFinished = true;
        }
    }
}
