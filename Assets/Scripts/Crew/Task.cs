using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public TaskType TaskType { get; set; }

    public Room Destination { get; set; }

   
    public Task(TaskType taskType, Room destination)
    {
        TaskType = taskType;
        Destination = destination;
    }
   
}
