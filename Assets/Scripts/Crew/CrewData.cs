using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.Scripts.Crew
{
    [CreateAssetMenu(fileName = "CrewData", menuName = "CrewData")]
    public class CrewData : ScriptableObject
    {
        public string crewName = "No Name";
        public string displayName = "Display Name";
        public float health;
        public float extinguishFireSkill;
        public float fixElectricFailureSkill;
        public float fixHullBreachSkill;
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
        
        [Header("New Game Values")]
        [SerializeField] private int _health = 100;
        [SerializeField] private bool _isHired = false;

        private void OnEnable()
        {
            Reset();
        }

        public void Reset()
        {
            health = _health;
            isHired = _isHired;
        }
    }
}
