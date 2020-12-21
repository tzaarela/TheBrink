using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CargoHoldSystem : ShipSystem
{
    public SystemType SystemType { get; set; }
    public PowerState PowerState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }
    public float AirLevel { get; set; }
    public float CurrentEnergyInSystem { get; set; }

    float Load;
    //Decide here, what should the load be? Look up how much the ship should carry.
    float MaxLoad;
    private Room systemRoom;

    public CargoHoldSystem(List<Room> rooms)
    {
        PowerState = PowerState.IsOn;
        SystemType = SystemType.CargoBay;
        systemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.CargoHold);


        EnergyWanted = 0;
    }

    public void Update()
    {
        AirLevel = systemRoom.OxygenLevel;
    }

    public void Run()
    {
        CurrentEnergyInSystem = CurrentEnergy;

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
