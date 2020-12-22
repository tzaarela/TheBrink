using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Crew
{
    [CreateAssetMenu(fileName = "CrewData", menuName = "CrewData")]
    public class CrewData : ScriptableObject
    {
        public string crewName = "No Name";
        public float health;
        public float repairSkill;
        public Sprite sprite;
        public Profession profession;

        public int age = -1;
        public GenderType gender;
        public string quirk = "????";
        public int lengthCM = -1;
        public BloodType bloodType;

        [Header("Contract Info")]
        public string companyName = "The Brink";
        public string favoriteFood = "????";
        public string workHours = "24/7, all year round";
        public string employeeHistory = "Unknown";
        public int salary = 1200;
        [TextArea] public string summary = "-";

        public bool isHired;
        
        [Header("DEBUG")]
        [SerializeField] private bool _beginWithDefault;
        [SerializeField] private bool _isHired = false;
        [SerializeField] private int _health = 100;

        private void OnEnable()
        {
            Reset();
        }

        public void Reset()
        {
            if (!_beginWithDefault)

                return;

            health = _health;
            isHired = _isHired;
        }
    }
}
