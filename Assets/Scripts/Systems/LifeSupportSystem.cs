using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSupportSystem : ShipSystem
{
    public float[] AirPerRoom;

    int amountOfRooms;
    float totalAirProduced;
    float portionOfAir;

    public LifeSupportSystem()
    {
        //This does not work, I need to get this some way right? Maybe I could just "get" it with some command?
        foreach (Room room in RoomController.Instance.Rooms)
        {
            amountOfRooms++;
        }


        //Dubbelkolla imorgon!
        AirPerRoom = new float[RoomController.Instance.Rooms.Count];

    }

    public void GetOxygenNeeded()
    {

    }
    public void ProduceOxygen()
    {

    }
    public void SendOxygenOut()
    {

    }
}
