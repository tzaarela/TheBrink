using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InterfacePanels
{
    public class ContextMenuPanel
    {

        [SerializeField]
        private GameObject menuItemPrefab;

        private List<Task> availableTasks;


        public ContextMenuPanel(List<Task> availableTasks)
        {
            this.availableTasks = availableTasks;
        }

        public void CreateMenuItems()
        {
            foreach (var task in availableTasks)
            {
                //CONTINUE
                //GameObject.Instantiate(menuItemPrefab, transform)
            }
           
        }
    }
}
