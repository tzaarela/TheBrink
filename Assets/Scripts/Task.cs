using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public TaskType TaskType { get; set; }

    public WayPoint WayPoint { get; set; }

    public Task(TaskType taskType, WayPoint wayPoint)
    {
        TaskType = taskType;
        WayPoint = wayPoint;
    }
   
}
