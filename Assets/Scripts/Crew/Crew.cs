using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Crew
{
    [CreateAssetMenu(fileName = "Crew", menuName = "Crew")]
    public class Crew : ScriptableObject
    {
        // public CrewData[] crewDataArray = new CrewData[3];
        public List<CrewData> crewDataList;

        public int GetCrewCount()
        {
            return crewDataList.Count;
        }
    }
}
