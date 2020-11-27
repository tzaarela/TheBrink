using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.InterfacePanels
{
    public class CrewPanel : EventTrigger
    {

        List<CrewMember> crewMembers;
        CrewPanelObject[] crewGameObjects;

        public void Start()
        {
            crewMembers = new List<CrewMember>();
            crewGameObjects = gameObject.GetComponentsInChildren<CrewPanelObject>();
            SetCrewGameObjects();
        }

        public void Update()
        {
            crewMembers = GetCrewMembers();

            foreach (var item in crewGameObjects)
            {
                if (item.CrewMember == null)
                {
                    SetCrewGameObjects();
                }
            }
        }

        public List<CrewMember> GetCrewMembers()
        {
            var crewMembers = CrewController.Instance.crewMembers;
            return crewMembers;
        }

        public void SetCrewGameObjects()
        {
            for (int i = 0; i < crewMembers.Count; i++)
            {
                crewGameObjects[i].CrewMember = crewMembers[i];
            }
        }

        
    }
}
