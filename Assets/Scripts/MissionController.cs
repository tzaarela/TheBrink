using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class MissionController : MonoBehaviour
    {

        Route route;
        

        public MissionController()
        {

        }

        public void Start()
        {
            route = new Route(1000, 5);
        }

        public void Update()
        {

        }

        public void UpdateShipPosition()
        {
            ShipController.Instance.Ship.Position += ShipController.Instance.Ship.Speed;
            route.ShipPosition = ShipController.Instance.Ship.Position;
        }

        public void CheckEncounters()
        {
            foreach (Encounter encounter in route.EncountersOnRoute)
            {

                if(!encounter.HasTriggered && route.ShipPosition > encounter.Position)
                {
                    encounter.HasTriggered = true;
                    encounter.Execute();
                }
            }
        }

    }

}
