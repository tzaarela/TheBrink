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
        [SerializeField] private Image _portrait;
        
        [SerializeField]
        GameObject crewNameText;

        [SerializeField]
        GameObject professionText;

        [SerializeField]
        GameObject healthBar;

        [SerializeField]
        GameObject statusText;

        [SerializeField]
        GameObject highlight;

        [SerializeField]
        GameObject dead;

        public CrewMember CrewMember { get => crewMember; set => crewMember = value; }

        private TextMeshProUGUI nameTextMesh;
        private TextMeshProUGUI professionTextMesh;
        private TextMeshProUGUI statusTextMesh;
        private Image healthBarImage;
        private bool isInit;

        private CrewMember crewMember;

        public void Start()
        {
            nameTextMesh = crewNameText.GetComponent<TextMeshProUGUI>();
            professionTextMesh = professionText.GetComponent<TextMeshProUGUI>();
            healthBarImage = healthBar.GetComponent<Image>();
            statusTextMesh = statusText.GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            if (CrewMember != null)
            {
                if(!isInit)
                {
                    crewMember.onDeath += HandleOnDeath;
                    isInit = true;
                }

                _portrait.sprite = crewMember.crewData.sprite;
                nameTextMesh.text = crewMember.crewData.name;
                professionTextMesh.text = crewMember.crewData.profession.ToString();
                statusTextMesh.text = crewMember.Status.ToString();

                healthBarImage.fillAmount = crewMember.crewData.health * 0.01f;

                if (crewMember.IsSelected)
                    highlight.SetActive(true);
                else
                    highlight.SetActive(false);
            }
        }

        public void HandleOnDeath()
        {
            dead.SetActive(true);
            crewMember.gameObject.SetActive(false);
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
