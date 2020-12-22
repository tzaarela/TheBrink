using Assets.Scripts.Rooms;
using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CargoHoldSystem : ShipSystem
{
    float Load;
    //Decide here, what should the load be? Look up how much the ship should carry.
    float MaxLoad;

    public CargoHoldSystem(List<Room> rooms)
    {
        PowerState = PowerState.IsOn;
        SystemType = SystemType.CargoBay;
        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.CargoHold);


        EnergyWanted = 0;
    }

    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }

    public override void Run()
    {
        CurrentEnergyInSystem = CurrentEnergy;

    }
}
