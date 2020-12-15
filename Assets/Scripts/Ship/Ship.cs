using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Ship")]
public class Ship : ScriptableObject
{
    public float speed = 10;
    public float position = 0;
    public float fuel = 1000;
    public float maxFuel = 1000;
    public float maxCapacitor = 1000;
    public float capacitor = 1000;
    public float capacitorBottleNeck;
    public int cash = 123;

    public RoomDataArray roomData;

    public List<float> GetRoomHealths()
    {
        return RoomController.Instance.Rooms.Where(x => x.RoomType != RoomType.Corridor).Select(x => x.RoomHealth).ToList();
    }

    public List<CrewMember> GetCrewMembers()
    {
        return CrewController.Instance.crewMembers;
    }

    public void SetCrewMember(int index, CrewMember crewMember)
    {
        CrewController.Instance.crewMembers[index] = crewMember;
    }
}
