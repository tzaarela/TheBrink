using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utility
{
    public abstract class Command 
    {
        public abstract void Execute();
        public abstract bool IsFinished { get; set; }
        public abstract string Name { get; set; }

        public abstract string StatusText { get; set; }
        public abstract Room Destination { get; set; }
    }
}
