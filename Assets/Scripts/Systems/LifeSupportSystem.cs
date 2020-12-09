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


    public LifeSupportSystem()
    {

    rooms = RoomController.Instance.Rooms;

    airMissingPerRoom = new float[rooms.Count];

    }

    public void LifeSupportUpdate()
    {
        GetOxygenNeeded();

        ProduceOxygen();

        SendOxygenOut();
    }
    public void GetOxygenNeeded()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].AirLevel < 95)
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

        while (totalAirProduced > totalOxygenNeeded || currentEnergy >= 2)
        {
            totalAirProduced++;

            currentEnergy -= 3;
        }

    }

    public void SendOxygenOut()
    {

        foreach (Room room in RoomController.Instance.Rooms)
        {

        }

    }
}
