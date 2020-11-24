using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TaskController : MonoBehaviour
    {

        public List<Task> tasks;

        public static TaskController instance;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);

                tasks = new List<Task>();
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddTask(TaskType taskType, WayPoint wayPoint, string crewName)
        {
            var task = new Task(taskType, wayPoint);
            tasks.Add(task);

            CrewController.Instance.crewMembers.First(x => x.Name == crewName).CurrentTask = task;
        }
    }
}
