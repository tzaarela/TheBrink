using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InterfacePanels
{
    public class ContextMenuPanel : MonoBehaviour
    {

        [SerializeField]
        private GameObject menuItemPrefab;

        private List<Task> availableTasks;


        //public ContextMenuPanel(List<Task> availableTasks)
        //{
        //    this.availableTasks = availableTasks;
        //}

        public void Start()
        {
            CreateMenuItems();
        }

        public void CreateMenuItems()
        {
            GameObject.Instantiate(menuItemPrefab, transform.position, Quaternion.identity, transform);
            //foreach (var task in availableTasks)
            //{
            //}
           
        }
    }
}
