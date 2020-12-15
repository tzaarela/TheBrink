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

        //[SerializeField]
        //float energyWanted;
        
        ShipSystem shipSystem;

        public void OnEnable()
        {
            
        }


        public void Awake()
        {
            
        }

        public void Start()
        {
            shipSystem = SystemController.Instance.ShipSystems.FirstOrDefault(x => x.SystemType == systemType);
        }

        public void Update()
        {
            airLevel.fillAmount = shipSystem.AirLevel * 0.01f;
            //energyLevel.fillAmount = shipSystem.CurrentEnergy
        }

        public void ToggleSystemPower()
        {
            shipSystem.SystemState = SystemState.IsOn;
        }
    }
}
