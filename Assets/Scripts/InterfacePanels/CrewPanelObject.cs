using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Assets.Scripts.InterfacePanels
{
    public class CrewPanelObject : UITrigger
    {
        [SerializeField]
        GameObject crewNameText;

        [SerializeField]
        GameObject professionText;

        [SerializeField]
        GameObject healthText;

        [SerializeField]
        GameObject statusText;

        [SerializeField]
        GameObject highlight;

        public CrewMember CrewMember { get => crewMember; set => crewMember = value; }

        private TextMeshProUGUI nameTextMesh;
        private TextMeshProUGUI professionTextMesh;
        private TextMeshProUGUI healthTextMesh;
        private TextMeshProUGUI statusTextMesh;

        private CrewMember crewMember;

        public void Start()
        {
            nameTextMesh = crewNameText.GetComponent<TextMeshProUGUI>();
            professionTextMesh = professionText.GetComponent<TextMeshProUGUI>();
            healthTextMesh = healthText.GetComponent<TextMeshProUGUI>();
            statusTextMesh = statusText.GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            if (CrewMember != null)
            {
                nameTextMesh.text = crewMember.crewData.name;
                professionTextMesh.text = crewMember.crewData.profession.ToString();
                healthTextMesh.text = crewMember.crewData.health.ToString();
                statusTextMesh.text = crewMember.Status.ToString();

                if (crewMember.IsSelected)
                    highlight.SetActive(true);
                else
                    highlight.SetActive(false);
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            crewMember.Select();

            base.OnPointerClick(eventData);
        }

        

        public override void OnPointerEnter(PointerEventData eventData)
        {
            this.transform.DOScale(1.01f, 0.25f);
            
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            this.transform.DOScale(1f, 0.25f);

            base.OnPointerExit(eventData);
        }


    }
}
