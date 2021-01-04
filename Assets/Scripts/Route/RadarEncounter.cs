using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Route
{
    public class RadarEncounter : MonoBehaviour
    {

        public Encounter encounter;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            encounter.Execute();
            Destroy(this.gameObject);
        }
    }
}
