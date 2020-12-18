using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionContract", menuName = "Mission Contract")]
public class MissionContract : ScriptableObject
{
    public string contractName;
    [TextArea] public string shortDescription;
    [TextArea] public string description;
    public string difficulty;
    [TextArea] public string difficultyDescription;
    public int averageFuelPercentNeeded;
    public int crewMembersNeeded;
    public int cashEarnings;
    public int bonusEarnings;
    public int threatPercent;
    public int alienLevel;
    public int pirateLevel;
    public int routeLength;
    public int routeTime;
}
