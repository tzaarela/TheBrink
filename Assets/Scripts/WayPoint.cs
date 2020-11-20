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
                TaskController.instance.AddTask(TaskType.Move, this, "John Doe");
            }
        }

    }
}
