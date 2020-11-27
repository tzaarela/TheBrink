using System.Collections.Generic;
using System.Linq;
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

        public void AddTask(TaskType taskType, Room room, string crewName)
        {
            var task = new Task(taskType, room);
            tasks.Add(task);

            CrewController.Instance.crewMembers.First(x => x.Name == crewName).CurrentTask = task;
        }
    }
}
