using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShipController
    {
        private static ShipController instance;

        public static ShipController Instance
        {
            get
            {
                if (instance == null)
                    instance = new ShipController();

                return instance;
            }
            set { instance = value; }
        }

        public Ship Ship { get; set; }

        public Ship CreateShip()
        {
            Ship = new Ship();
            return Ship;
        }
    }
}
