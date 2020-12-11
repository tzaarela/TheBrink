using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Assets.Scripts.Utility;

namespace Assets.Scripts.InterfacePanels
{
    public class ContextMenuPanel : MonoBehaviour
    {
        [SerializeField]
        private ContextMenuItem menuItemPrefab;

        //public ContextMenuPanel(List<Task> availableTasks)
        //{
        //    this.availableTasks = availableTasks;

        public void CreateMenuItems(List<Command> availableTasks)
        {
            foreach (Command command in availableTasks)
            {
                var menuItem = Instantiate(menuItemPrefab, transform.position, Quaternion.identity, transform);
                menuItem.Command = command;
                menuItem.GetComponentInChildren<TextMeshProUGUI>().text = command.Name.ToString();
            }
                     
        }
    }
}
