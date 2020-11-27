using Assets.Scripts.InterfacePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Systems
{
    public class SystemTab : UITrigger
    {
        private bool isSelected;
        private GameObject background;
        private SystemTabController SystemTabController;

        public bool IsSelected
        {
            get { return isSelected; }
            set 
            { 
                isSelected = value;
                if (isSelected)
                    background.SetActive(true);
                else
                    background.SetActive(false);

            }
        }

        public void Start()
        {
            background = transform.Find("Background").gameObject;
            SystemTabController = transform.parent.gameObject.GetComponent<SystemTabController>();
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
