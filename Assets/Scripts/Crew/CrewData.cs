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
        public string crewName;
        public float health;
        public float repairSkill;
        public Sprite sprite;
        public Profession profession;
    }
}
