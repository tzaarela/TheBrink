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
        public float RoomHealth { get; set; }
        public SystemType SystemType { get; set; }
        public PowerState PowerState { get; set; }
        public bool IsDepressurised { get; set; }
        public float AirLevel { get; set; }

        public float UpkeepCost { get; set; }
        public float Capacity { get; set; }
        public float CurrentEnergy { get; set; }
        public float MaxEnergy { get; set; }

        public virtual void Reboot()
        {
        }

        public virtual void Run()
        {
            RoomHealth = SystemRoom.data.health;
        }

        public virtual void SetCapacity()
        {
            if(CurrentEnergy < 20)
            {
                Capacity = 0.5f;
            }
            else if(CurrentEnergy < 40)
            {
                Capacity = 1.0f;

            }
            else if(CurrentEnergy < 60)
            {
                Capacity = 1.3f;

            }
            else if (CurrentEnergy < 90)
            {
                Capacity = 1.6f;

            }
            else
            {
                Capacity = 2.0f;
            }
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
