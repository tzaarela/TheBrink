﻿using System;
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
        public List<CrewData> crewDataList;

        public int GetCrewCount()
        {
            return crewDataList.Count;
        }

        public void Reset()
        {
            crewDataList = new List<CrewData>();
        }
    }
}
