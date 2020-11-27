﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.EventSystems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.InterfacePanels
{
    public class ContextMenuItem : UITrigger
    {
        Image image;

        bool _isHighlighted;

        private Task task;

        public Task Task
        {
            get { return task; }
            set { task = value; }
        }



        [SerializeField]
        private Color32 highlightColor;

        [SerializeField]
        private Color32 backColor;


        public bool IsHighlighted
        {
            get
            {
                return _isHighlighted;
            }

            set
            {
                _isHighlighted = value;
                if (_isHighlighted)
                    HighlightMenuItem(true);
                else
                    HighlightMenuItem(false);
            }
        }

        public void Start()
        {
            image = transform.GetComponent<Image>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Debug.LogError($"{name} OnPointerClick()...");
            CrewController.Instance.GetSelectedCrewMember().CurrentTask = Task;
            ContextMenuController.instance.CloseContextMenu();

            base.OnPointerClick(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            IsHighlighted = true;
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            IsHighlighted = false;
            base.OnPointerExit(eventData);
        }

        public void HighlightMenuItem(bool isHighlight)
        {
            if (isHighlight)
                image.color = highlightColor;
            else
                image.color = backColor;
        }
    }
}
