using Assets.Scripts.InterfacePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Assets.Scripts.Systems
{
    public class SystemTab : UITrigger
    {
        private bool isSelected;
        private GameObject background;
        private TextMeshProUGUI tabText;
        private SystemTabController SystemTabController;
        

        [SerializeField]
        private SystemType systemType;

        [SerializeField]
        Color32 textColorSelected;

        [SerializeField]
        Color32 textColorUnselected;

        [SerializeField]
        GameObject reactorContent;

        [SerializeField]
        GameObject weaponsContent;

        public bool IsSelected
        {
            get { return isSelected; }
            set 
            { 
                isSelected = value;
                if (isSelected)
                {
                    
                    background.SetActive(true);
                    tabText.color = textColorSelected;
                    SetContent(systemType);
                }
                else
                {
                    background.SetActive(false);
                    tabText.color = textColorUnselected;
                }

            }
        }

        public void Start()
        {
            background = transform.Find("Background").gameObject;
            tabText = transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

            SystemTabController = transform.parent.gameObject.GetComponent<SystemTabController>();
        }

        public void DisableAllContent()
        {
            //reactorContent.SetActive(false);
            //weaponsContent.SetActive(false);
        }

        public void SetContent(SystemType systemType)
        {
            DisableAllContent();

            switch (systemType)
            {
                //case SystemType.Reactor:
                //    reactorContent.SetActive(true);
                //    break;
                //case SystemType.Weapons:
                //    weaponsContent.SetActive(true);
                //    break;
                //case SystemType.Mainframe:
                //    break;
                //case SystemType.Hull:
                //    break;
                //case SystemType.Medbay:
                //    break;
                //case SystemType.Resources:
                //    break;
                //case SystemType.Navigator:
                //    break;
                //default:
                //    break;
            }
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            if(!isSelected)
            {
                SystemTabController.DeselectActive();
                SystemTabController.Select(this);
            }
            base.OnPointerClick(eventData);
        }
    }
}
