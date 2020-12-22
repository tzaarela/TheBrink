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
    public float[] oxygenMissingPerRoom;
    public List<Room> rooms;

    float totalOxygenProduced;
    float totalOxygenNeeded;

    private Debugger debugger;

    public LifeSupportSystem(List<Room> rooms)
    {
        PowerState = PowerState.IsOn;
        SystemType = SystemType.LifeSupport;
        this.rooms = rooms;
        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.LifeSupport);
        oxygenMissingPerRoom = new float[rooms.Count];
        EnergyWanted = 0;
    }

    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }

    public override void Run()
    {
        GetOxygenNeeded();

        var oxygenFragment = ProduceOxygen();
        SendOxygenOut(oxygenFragment);

        SetEnergyWanted();

        CurrentEnergyInSystem = CurrentEnergy;
    }

    public void GetOxygenNeeded()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].OxygenLevel < SystemController.Instance.optimalOxygenLevel)
            {
                oxygenMissingPerRoom[i] = 100 - rooms[i].OxygenLevel;
            }
        }
    }

    public float ProduceOxygen()
    {
        totalOxygenNeeded = 0;
        totalOxygenProduced = 0;

        for (int i = 0; i < oxygenMissingPerRoom.Length; i++)
        {
            totalOxygenNeeded += oxygenMissingPerRoom[i];
        }

        while (totalOxygenProduced <= totalOxygenNeeded && CurrentEnergy > SystemController.Instance.oxygenProduceCost)
        {
            totalOxygenProduced += 6;

            CurrentEnergy -= SystemController.Instance.oxygenProduceCost;
        }

        if(totalOxygenNeeded == 0)
        {
            return 0;
        }
        //TODO: DJ, look at this later, very weird.
        return totalOxygenProduced / totalOxygenNeeded;
    }

    public void SendOxygenOut(float oxygenFragment)
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            rooms[i].OxygenLevel += oxygenFragment * oxygenMissingPerRoom[i];
        }
    }

    public override void SetEnergyWanted()
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
}