using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.InterfacePanels
{
    public class ContextMenuPanel : MonoBehaviour
    {
        [SerializeField]
        private ContextMenuItem menuItemPrefab;

        //public ContextMenuPanel(List<Task> availableTasks)
        //{
        //    this.availableTasks = availableTasks;

        public void CreateMenuItems(List<Task> availableTasks)
        {
            foreach (Task task in availableTasks)
            {
                var menuItem = Instantiate(menuItemPrefab, transform.position, Quaternion.identity, transform);
                menuItem.Task = task;
                menuItem.GetComponentInChildren<TextMeshProUGUI>().text = task.TaskType.ToString();
            }
                     
        }
    }
}
