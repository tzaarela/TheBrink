using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Scripts.InterfacePanels
{
    public class CrewPanelObject : UITrigger
    {
        [SerializeField]
        GameObject crewName;

        [SerializeField]
        GameObject profession;

        [SerializeField]
        GameObject health;

        [SerializeField]
        Image highlight;

        public CrewMember CrewMember { get => crewMember; set => crewMember = value; }

        private TextMeshProUGUI nameTextMesh;
        private TextMeshProUGUI professionTextMesh;
        private TextMeshProUGUI healthTextMesh;
        private CrewMember crewMember;

        public void Start()
        {
            nameTextMesh = crewName.GetComponent<TextMeshProUGUI>();
            professionTextMesh = profession.GetComponent<TextMeshProUGUI>();
            healthTextMesh = health.GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            if (CrewMember != null)
            {
                nameTextMesh.text = crewMember.Name;
                professionTextMesh.text = crewMember.Profession.ToString();
                healthTextMesh.text = crewMember.Health.ToString();

                if (crewMember.IsSelected)
                    highlight.color = Color.green;
                else
                    highlight.color = Color.red;
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            CrewController.Instance.crewMembers.ForEach(x => x.IsSelected = false);
            crewMember.IsSelected = true;

            base.OnPointerClick(eventData);
        }


    }
}
