using Assets.Scripts.Controls;
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

        [SerializeField]
        ToggleButton lockButton;

        IShipSystem shipSystem;
        bool isInitalized;

        public GameObject specificControlPrefab;
        private IControls specificControl;

        public void Init()
        {
            shipSystem = SystemController.Instance.ShipSystems.FirstOrDefault(x => x.SystemType == systemType);
            powerButton.onToggle += ToggleSystemPower;
            depressurizeButton.onToggle += ToggleDepressurise;
            lockButton.onToggle += ToggleLock;
            specificControl = specificControlPrefab.GetComponent<IControls>();
            specificControl.Init();
        }

        public void UpdateUI()
        {
            //If u get an exception here it´s probably because u loaded wrong scene without debug mode.
            airLevel.fillAmount = shipSystem.AirLevel * 0.01f;
            energyLevel.fillAmount = shipSystem.CurrentEnergy * 0.01f;

            specificControl.UpdateUI();
        }

        //If on, turn off, if off turn on.
        public void ToggleSystemPower(bool isOn)
        {
            shipSystem.PowerState = isOn ? PowerState.IsOn : PowerState.IsOff;
        }

        public void ToggleDepressurise(bool isOn)
        {
            shipSystem.IsDepressurised = isOn;
            shipSystem.SystemRoom.RoomState = isOn ? RoomState.Depreassurized : RoomState.Open;
        }

        public void ToggleLock(bool isOn)
        {
            if (isOn)
                shipSystem.SystemRoom.LockRoom();
            else
                shipSystem.SystemRoom.UnlockRoom();
        }
    }
}