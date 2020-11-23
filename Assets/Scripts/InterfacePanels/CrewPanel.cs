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
    public class CrewPanel : InterfaceTrigger
    {

        List<CrewMember> crewMembers;
        CrewPanelObject[] crewGameObjects;

        public CrewPanel()
        {
            crewMembers = new List<CrewMember>();
            //crewGameObjects = gameObject.GetComponentsInChildren<GameObject>();
        }

        public void Update()
        {
            crewMembers = GetCrewMembers();
        }

        public List<CrewMember> GetCrewMembers()
        {
            var crewMembers = CrewController.Instance.crewMembers;
            return crewMembers;
        }

        public void CreateCrewGameObjects()
        {
            for (int i = 0; i < crewMembers.Count; i++)
            {
                //var textMesh = crewGameObjects[i].GetComponentInChildren<TextMeshProUGUI>();
                //textMesh.text = crewMembers[i].Name;
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            var crewGameObject = eventData.selectedObject.gameObject;

            if(crewGameObject.tag == "CrewMember")
            {
                //crewGameObject.Get;
            }

            base.OnPointerClick(eventData);
        }


    }
}
