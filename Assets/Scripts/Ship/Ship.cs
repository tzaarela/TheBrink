using System;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Ship", menuName = "Ship")]
public class Ship : ScriptableObject
{
    public float speed = 10f;
    public float position = 0f;
    public float fuel = 1000f;
    public float maxFuel = 1000f;
    public float capacitor = 1000f;
    public float maxCapacitor = 1000f;
    public float capacitorBottleNeck;
    public int cash = 1000000;
    public string captainName = "John";


    public MissionContract missionContract;
    public RoomDataArray roomData;
    
    [Header("DEBUG")]
    [SerializeField] private float _defaultSpeed = 10f;
    [SerializeField] private float _defaultPosition = 0f;
    [SerializeField] private float _defaultFuel = 568f;
    [SerializeField] private float _defaultCapacitor = 1000f;
    [SerializeField] private int _defaultCash = 1000000;
    [SerializeField] private bool _beginWithDefault;

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        if (_beginWithDefault)
        {
            speed = _defaultSpeed;
            position = _defaultPosition;
            fuel = _defaultFuel;
            capacitor = _defaultCapacitor;
            cash = _defaultCash;
            return;
        }
        
        position = 0;
        cash = 1000000;
        fuel = maxFuel;
        capacitor = maxCapacitor;
    }

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

    public float GetFuelPercent()
    {
        return fuel / maxFuel;
    }

    public int GetCrewCount()
    {
        return 99; // TODO DEBUG ONLY
    }
}
