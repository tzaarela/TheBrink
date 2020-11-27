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
        GameObject highlight;

        public CrewMember CrewMember { get => crewMember; set => crewMember = value; }

        private TextMeshProUGUI nameTextMesh;
        private TextMeshProUGUI professionTextMesh;
        private TextMeshProUGUI healthTextMesh;
        private CrewMember crewMember;

        public void Start()
        {
            nameTextMesh = crewNameText.GetComponent<TextMeshProUGUI>();
            professionTextMesh = professionText.GetComponent<TextMeshProUGUI>();
            healthTextMesh = healthText.GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            if (CrewMember != null)
            {
                nameTextMesh.text = crewMember.Name;
                professionTextMesh.text = crewMember.Profession.ToString();
                healthTextMesh.text = crewMember.Health.ToString();

                if (crewMember.IsSelected)
                    highlight.SetActive(true);
                else
                    highlight.SetActive(false);
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log(crewName + " is selected");
            CrewController.Instance.crewMembers.ForEach(x => x.IsSelected = false);
            crewMember.IsSelected = true;

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
