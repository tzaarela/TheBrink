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
        private GameObject menuItemPrefab;

        //public ContextMenuPanel(List<Task> availableTasks)
        //{
        //    this.availableTasks = availableTasks;

        public void CreateMenuItems(List<Task> availableTasks)
        {
            foreach (Task task in availableTasks)
            {
                menuItemPrefab.GetComponentInChildren<TextMeshProUGUI>().text = task.TaskType.ToString();
                GameObject.Instantiate(menuItemPrefab, transform.position, Quaternion.identity, transform);
            }
                     
        }
    }
}
