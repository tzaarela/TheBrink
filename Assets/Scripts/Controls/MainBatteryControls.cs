using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controls
{
    public class MainBatteryControls : MonoBehaviour, IControls
    {
        [SerializeField]
        Button fireButton;

        [SerializeField]
        Button lockTarget;

        MainBatterySystem mainBatterySystem;

        public IShipSystem shipSystem { get; set; }

        public void Init()
        {
            mainBatterySystem = SystemController.Instance.ShipSystems
                .FirstOrDefault(x => x.SystemType == SystemType.MainBattery)
                as MainBatterySystem;

            fireButton.onClick.AddListener(mainBatterySystem.Fire);
            lockTarget.onClick.AddListener(mainBatterySystem.LockOn);
        }

        public void UpdateUI()
        {
        }
    }
}
