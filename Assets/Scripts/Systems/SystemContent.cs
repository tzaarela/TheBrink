using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Systems
{
    public class SystemContent : MonoBehaviour
    {
        [SerializeField]
        SystemType systemType;

        [SerializeField]
        Image airLevel;

        [SerializeField]
        Image energyLevel;

        [SerializeField]
        ToggleButton powerButton;

        [SerializeField]
        ToggleButton depressurizeButton;

        IShipSystem shipSystem;

        public void Start()
        {
            shipSystem = SystemController.Instance.ShipSystems.FirstOrDefault(x => x.SystemType == systemType);
            powerButton.onToggle += ToggleSystemPower;
            depressurizeButton.onToggle += ToggleDepressurise;
        }

        public void Update()
        {
            //If u get an exception here it´s probably because u loaded wrong scene without debug mode.
            airLevel.fillAmount = shipSystem.AirLevel * 0.01f;
            energyLevel.fillAmount = shipSystem.CurrentEnergyInSystem * 0.01f;
        }

        //If on, turn off, if off turn on.
        public void ToggleSystemPower(bool isOn)
        {
            shipSystem.PowerState = isOn ? PowerState.IsOn : PowerState.IsOff;
        }

        public void ToggleDepressurise(bool isOn)
        {
            shipSystem.IsDepressurised = isOn;
        }
    }
}