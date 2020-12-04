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
            // Debug.Log($"{name} OnPointerClick()...");
            var crewMember = CrewController.Instance.GetSelectedCrewMember();
            if (crewMember == null)
            {
                ContextMenuController.instance.CloseContextMenu();
                return;
            }

            if (Task.TaskType == TaskType.Repair && crewMember.CurrentWayPoint != Task.Destination.Waypoint)
                crewMember.AddTask(new Task(TaskType.Move, Task.Destination));

            crewMember.AddTask(Task);

            ContextMenuController.instance.CloseContextMenu();

            //base.OnPointerClick(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            IsHighlighted = true;
            ContextMenuController.instance.IsMenuItemSelected = true;
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            IsHighlighted = false;
            ContextMenuController.instance.IsMenuItemSelected = false;
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
