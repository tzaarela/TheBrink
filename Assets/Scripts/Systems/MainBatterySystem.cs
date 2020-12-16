using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainBatterySystem : ShipSystem
{
    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }
    public float AirLevel { get; set; }
    private Room systemRoom;

    public MainBatterySystem(List<Room> rooms)
    {
        systemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.MainBattery);

        SystemState = SystemState.IsOn;
        SystemType = SystemType.MainBattery;

        EnergyWanted = 0;

    }

    public void Run()
    {
        AirLevel = systemRoom.OxygenLevel;
    }

    public void SetEnergyWanted()
    {
        throw new System.NotImplementedException();
    }

    public void Reboot()
    {
        throw new System.NotImplementedException();
    }

    public void RunDiagnostic()
    {
        throw new System.NotImplementedException();
    }
}