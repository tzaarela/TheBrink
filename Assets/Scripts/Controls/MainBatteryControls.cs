using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controls
{
    public class MainBatteryControls : MonoBehaviour
    {
        [SerializeField]
        Button fireButton;

        [SerializeField]
        Button lockTarget;

        MainBatterySystem mainBatterySystem;

        public void Start()
        {
            mainBatterySystem = SystemController.Instance.ShipSystems
                .FirstOrDefault(x => x.SystemType == SystemType.MainBattery)
                as MainBatterySystem;

            fireButton.onClick.AddListener(mainBatterySystem.Fire);
            lockTarget.onClick.AddListener(mainBatterySystem.LockOn);
        }
    }
}
