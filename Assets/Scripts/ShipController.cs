using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShipController : MonoBehaviour
    {
        public static ShipController Instance { get; set; }

        public Ship Ship { get; set; }

        void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                Ship = CreateShip();
            }
                
            else
                Destroy(this);
        }

        public Ship CreateShip()
        {
            return new Ship();
        }
    }
}
