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
        private ShipSystem shipSystem;

        [SerializeField]
        Color32 textColorSelected;

        [SerializeField]
        Color32 textColorUnselected;

        [SerializeField]
        SystemContent content;

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
                    ActivateContent();
                }
                else
                {
                    background.SetActive(false);
                    tabText.color = textColorUnselected;
                    DisableContent();
                }

            }
        }

        public void Start()
        {
            background = transform.Find("Background").gameObject;
            tabText = transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

            SystemTabController = transform.parent.gameObject.GetComponent<SystemTabController>();
        }

        public void DisableContent()
        {
            content.gameObject.SetActive(false);
        }

        public void ActivateContent()
        {
            content.gameObject.SetActive(true);
        }
    }
}
