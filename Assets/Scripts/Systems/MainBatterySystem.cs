using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainBatterySystem : ShipSystem
{
    public SystemType SystemType { get; set; }
    public PowerState PowerState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }
    public float AirLevel { get; set; }
    private Room systemRoom;

    public MainBatterySystem(List<Room> rooms)
    {
        systemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.MainBattery);

        PowerState = PowerState.IsOn;
        SystemType = SystemType.MainBattery;

        EnergyWanted = 0;

    }

    public void Update()
    {
        AirLevel = systemRoom.oxygenLevel;
    }

    public void Run()
    {
        AirLevel = systemRoom.oxygenLevel;
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