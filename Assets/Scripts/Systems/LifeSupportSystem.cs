using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSupportSystem : ShipSystem
{
    public float[] airMissingPerRoom;
    public List<Room> rooms;

    float totalAirProduced;
    float portionOfAir;
    float totalOxygenNeeded;

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public LifeSupportSystem()
    {
        SystemState = SystemState.IsOn;

        rooms = RoomController.Instance.Rooms;

        airMissingPerRoom = new float[rooms.Count];

    }

    public void GetOxygenNeeded()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].AirLevel < SystemController.Instance.optimalAirLevel)
            {
                airMissingPerRoom[i] = 100 - rooms[i].AirLevel;
            }
        }
    }

    public void ProduceOxygen()
    {
        for (int i = 0; i < airMissingPerRoom.Length; i++)
        {
            totalOxygenNeeded += airMissingPerRoom[i];
        }

        while (totalAirProduced > totalOxygenNeeded || CurrentEnergy > SystemController.Instance.airProduceCost)
        {
            totalAirProduced++;

            CurrentEnergy -= SystemController.Instance.airProduceCost;
        }

    }

    public void SendOxygenOut()
    {

        foreach (Room room in RoomController.Instance.Rooms)
        {

        }

    }

    public void Run()
    {
        GetOxygenNeeded();

        ProduceOxygen();

        SendOxygenOut();
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
