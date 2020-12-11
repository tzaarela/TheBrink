using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Crew
{
    class IdleCommand : Command
    {
        public override string Name { get; set; }
        public override Room Destination { get; set; }
        public override bool IsFinished { get; set; }
        public override string StatusText { get; set; }

        public override void Execute()
        {
           
        }
    }
}
