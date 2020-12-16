using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeSupportSystem : ShipSystem
{
    public float[] oxygenMissingPerRoom;
    public List<Room> rooms;

    float totalOxygenProduced;
    float totalOxygenNeeded;

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }

    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public float AirLevel { get; set; }

    private Room systemRoom;

    public LifeSupportSystem(List<Room> rooms)
    {
        SystemState = SystemState.IsOn;
        SystemType = SystemType.LifeSupport;
        this.rooms = rooms;
        systemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.LifeSupport);

        oxygenMissingPerRoom = new float[rooms.Count];

        EnergyWanted = 0;
    }

    public void Run()
    {

        AirLevel = systemRoom.oxygenLevel;

        GetOxygenNeeded();

        var oxygenFragment = ProduceOxygen();

        SendOxygenOut(oxygenFragment);

        SetEnergyWanted();
    }

    public void GetOxygenNeeded()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].oxygenLevel < SystemController.Instance.optimalOxygenLevel)
            {
                oxygenMissingPerRoom[i] = 100 - rooms[i].oxygenLevel;
            }
        }
    }

    public float ProduceOxygen()
    {
        for (int i = 0; i < oxygenMissingPerRoom.Length; i++)
        {
            totalOxygenNeeded += oxygenMissingPerRoom[i];
        }

        while (totalOxygenProduced < totalOxygenNeeded || CurrentEnergy > SystemController.Instance.oxygenProduceCost)
        {
            totalOxygenProduced++;

            CurrentEnergy -= SystemController.Instance.oxygenProduceCost;
        }

        if(totalOxygenNeeded == 0)
        {
            return 0;
        }

        return totalOxygenProduced / totalOxygenNeeded;
    }

    public void SendOxygenOut(float oxygenFragment)
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            rooms[i].oxygenLevel += oxygenFragment * oxygenMissingPerRoom[i];
        }
    }

    public void SetEnergyWanted()
    {
        if (totalOxygenNeeded > totalOxygenProduced)
        {
            EnergyWanted = (totalOxygenNeeded - totalOxygenProduced) * SystemController.Instance.oxygenProduceCost;
        }
        else
        {
            EnergyWanted = 0;
        }
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