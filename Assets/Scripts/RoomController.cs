using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<Room> Rooms { get; set; }

    public static RoomController Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void CreateBreachInRoom(float severity)
    {
        //int _indexOf 1;

        float _severityAmount = severity;
        //I do wonder if I've been stupid here, maybe I don't need to send with roomtype,
        //Maybe that is something that RoomController should look into instead,

        

        //Rooms[_indexOf].CreateHazard(HazardType.Breach, _severityAmount);
    }
}
