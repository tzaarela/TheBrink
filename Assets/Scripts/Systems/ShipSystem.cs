using Assets.Scripts.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Systems
{
    public class ShipSystem : IShipSystem
    {
        public Room SystemRoom { get; set; }
        public SystemType SystemType { get; set; }
        public PowerState PowerState { get; set; }
        public bool IsDepressurised { get; set; }
        public float AirLevel { get; set; }

        public float UpkeepCost { get; set; }
        public float CurrentEnergy { get; set; }

        public virtual void Reboot()
        {
        }

        public virtual void Run()
        {
            
        }

        public virtual void Upkeep()
        {
            CurrentEnergy -= UpkeepCost;
        }

        public virtual void RunDiagnostic()
        {
        }

        public virtual void Update()
        {
            if (IsDepressurised)
                SystemRoom.OxygenLevel -= 1f;
        }
    }
}
