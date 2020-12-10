using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSupportSystem : ShipSystem
{
    public float[] airMissingPerRoom;
    public List<Room> rooms;

    float totalOxygenProduced;
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
    
    public void Run()
    {
        GetOxygenNeeded();

        var oxygenFragment = ProduceOxygen();

        SendOxygenOut(oxygenFragment);

        SetEnergyWanted();
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

    public float ProduceOxygen()
    {
        for (int i = 0; i < airMissingPerRoom.Length; i++)
        {
            totalOxygenNeeded += airMissingPerRoom[i];
        }

        while (totalOxygenProduced < totalOxygenNeeded || CurrentEnergy > SystemController.Instance.airProduceCost)
        {
            totalOxygenProduced++;

            CurrentEnergy -= SystemController.Instance.airProduceCost;
        }
        return totalOxygenProduced / totalOxygenNeeded;
    }

    public void SendOxygenOut(float oxygenFragment)
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            rooms[i].AirLevel += oxygenFragment * airMissingPerRoom[i];
        }
    }

    public void SetEnergyWanted()
    {
        if (totalOxygenNeeded > totalOxygenProduced)
        {
            EnergyWanted = (totalOxygenNeeded - totalOxygenProduced) * SystemController.Instance.airProduceCost;
        }

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