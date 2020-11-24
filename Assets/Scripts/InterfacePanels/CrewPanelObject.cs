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
    public class CrewPanelObject : InterfaceTrigger
    {
        [SerializeField]
        GameObject crewName;

        [SerializeField]
        GameObject profession;

        [SerializeField]
        GameObject health;

        [SerializeField]
        Image highlight;

        public CrewMember CrewMember { get; set; }

        private TextMeshProUGUI nameTextMesh;
        private TextMeshProUGUI professionTextMesh;
        private TextMeshProUGUI healthTextMesh;

        public void Start()
        {
            nameTextMesh = crewName.GetComponent<TextMeshProUGUI>();
            professionTextMesh = profession.GetComponent<TextMeshProUGUI>();
            healthTextMesh = health.GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            if(CrewMember != null)
            {
                nameTextMesh.text = CrewMember.Name;
                professionTextMesh.text = CrewMember.Profession.ToString();
                healthTextMesh.text = CrewMember.Health.ToString();

                if (CrewMember.IsSelected)
                    highlight.color = Color.green;
                else
                    highlight.color = Color.red;
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            CrewController.Instance.crewMembers.ForEach(x => x.IsSelected = false);
            CrewMember.IsSelected = true;

            base.OnPointerClick(eventData);
        }


    }
}
