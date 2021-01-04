using System;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.InterfacePanels;

namespace Assets.Scripts.Controls
{
    public class ReactorControls : UITrigger, IControls
    {
        Ship ship;
        ReactorSystem reactorSystem;

        [SerializeField]
        private TextMeshProUGUI fuelValue;

        [SerializeField]
        private TextMeshProUGUI speedValue;

        [SerializeField]
        private Button buttonThrottleUp;

        [SerializeField]
        private Button buttonThrottleDown;

        public IShipSystem shipSystem { get; set; }

        public void ThrottleUp()
        {
            reactorSystem.CapacityLevel += 1;
        }

        public void ThrottleDown()
        {
            reactorSystem.CapacityLevel -= 1;

        }

        public void UpdateUI()
        {
            fuelValue.text = Mathf.CeilToInt(ship.fuel).ToString();
            speedValue.text = ship.speed.ToString() + " AU/s";
        }

        public void Init()
        {
            ship = GameController.Instance.ship;
            reactorSystem = SystemController.Instance.ShipSystems.FirstOrDefault(x => x.SystemType == SystemType.Reactor) as ReactorSystem;
            buttonThrottleUp.onClick.AddListener(ThrottleUp);
            buttonThrottleDown.onClick.AddListener(ThrottleDown);
        }
    }
}
