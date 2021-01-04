using Assets.Scripts;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using Assets.Scripts.Systems;

public class LifeSupportSystem : ShipSystem
{
    public List<Room> rooms;

    private Debugger debugger;

    float oxygenProduced;

    public LifeSupportSystem(List<Room> rooms)
    {
        PowerState = PowerState.IsOn;
        SystemType = SystemType.LifeSupport;
        this.rooms = rooms;
        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.LifeSupport);

        UpkeepCost = SystemController.Instance.lifeSupportUpkeepSystem;
        CurrentEnergy = 50;

    }

    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }

    public override void Run()
    {

        SendOutOxygen();
    }

    public void SetEfficiency()
    {

    }

    public void SendOutOxygen()
    {
        foreach (var room in rooms)
        {
            //Checks if we even have enough energy in the system to produce oxygen, else breaks the foreach loop.
            if (CurrentEnergy > SystemController.Instance.oxygenProduceCost)
            {
                //Checks if each room has enough oxygen, and isn't being sealed or depressurized.
                if (room.OxygenLevel < SystemController.Instance.optimalOxygenLevel && room.RoomState == RoomState.Open)
                {
                    //Gives a portion of oxygen multiplied with the systems Capacity.
                    room.OxygenLevel += SystemController.Instance.oxygenProduced * Capacity;
                }
            }
            else
            {
                break;
            }
        }
    }
}