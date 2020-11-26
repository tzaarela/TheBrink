using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public TaskType TaskType { get; set; }

    public Waypoint Waypoint { get; set; }

    public Task(TaskType taskType, Waypoint waypoint)
    {
        TaskType = taskType;
        Waypoint = waypoint;
    }
   
}
