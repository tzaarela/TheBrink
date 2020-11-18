using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard
{
    public float SeverityAmount { get; set; }

    public HazardType HazardType { get; set; }

    public bool IsFinished { get; set; }

    public Hazard(HazardType hazardType, float severityAmount)
    {
        this.HazardType = hazardType;
        this.SeverityAmount = severityAmount;
    }

    public void ExecuteHazard()
    {

    }
}
