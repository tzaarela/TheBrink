using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Controls
{
    class ReactorControls : MonoBehaviour
    {
        Ship ship;

        [SerializeField]
        private TextMeshProUGUI fuelValue;

        public void Start()
        {
            ship = GameController.Instance.ship;
        }

        public void Update()
        {
            fuelValue.text = Mathf.CeilToInt(ship.fuel).ToString();
        }
    }
}
