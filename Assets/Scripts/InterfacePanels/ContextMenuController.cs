﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InterfacePanels
{
    public class ContextMenuController : MonoBehaviour
    {
        public static ContextMenuController instance;

        private bool isOpen;

        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }

        [SerializeField]
        private Canvas printToCanvas;

        [SerializeField]
        private GameObject menuPanelPrefab;

        private GameObject menuPanelObject;

        private Camera _camera;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        public void OpenContextMenu(List<Task> availableTasks)
        {
            CloseContextMenu();

            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            menuPanelObject = Instantiate(menuPanelPrefab, new Vector2(mousePos.x, mousePos.y), Quaternion.identity, printToCanvas.transform);
            var menuPanel = menuPanelObject.GetComponent<ContextMenuPanel>();

            menuPanel.CreateMenuItems(availableTasks);
        }

        public void CloseContextMenu()
        {
            if (menuPanelObject != null)
                Destroy(menuPanelObject);
        }
    }
}
