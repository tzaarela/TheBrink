using Assets.Scripts.InterfacePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    
    public class WayPoint : MonoBehaviour
    {
        [SerializeField]
        public List<WayPoint> Neighbours;

        [SerializeField]
        private ContextMenuPanel contextMenuPanel;

        [SerializeField]
        private GameObject contextCanvas;

        public Vector2 Position { get; set; }

        public bool isSelected;

        public void Start()
        {
            Position = transform.position;
        }

        public void OnMouseOver()
        {
            Debug.Log("MouseOver");

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Adding move task");

                var crewName = CrewController.Instance.crewMembers.First(x => x.IsSelected).Name;
                TaskController.instance.AddTask(TaskType.Move, this, crewName);
            }

            if (Input.GetMouseButtonDown(1))
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(contextMenuPanel, new Vector2(mousePos.x, mousePos.y), Quaternion.identity, contextCanvas.transform);
            }
        }

    }
}
