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
        Button powerButton;

        //[SerializeField]
        //float energyWanted;
        
        ShipSystem shipSystem;

        public void Start()
        {
            shipSystem = SystemController.Instance.ShipSystems.FirstOrDefault(x => x.SystemType == systemType);
        }

        public void Update()
        {
            //If u get an exception here it´s probably because u loaded wrong scene without debug mode.
            airLevel.fillAmount = shipSystem.AirLevel * 0.01f;
            //energyLevel.fillAmount = shipSystem.CurrentEnergy
        }
        //If on, turn off, if off turn on.
        public void ToggleSystemPower()
        {
            
            shipSystem.SystemState = SystemState.IsOn;
        }
    }
}
