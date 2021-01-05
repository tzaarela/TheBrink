using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Systems;

public class CorridorSystem : ShipSystem
{
    public CorridorSystem(List<Room> rooms)
    {
        PowerState = PowerState.IsOn;
        SystemType = SystemType.Corridors;
        SystemRoom = rooms.FirstOrDefault(x => x.SystemType == SystemType.Corridors);

        CurrentEnergy = 50;


        /*
         * It needs a list of all corridors & airlocks.
         * Goes through the list, and updates the airvalues etc on each.
         * Remember that they update their air in Life Support, so here we only need to update the amount.
         * 
         * And then, we need to be able to close and open doors from here. There is a bool I think? Right?
         * 
         * But how does it know which door? And this method should maybe be on ALL shipSystems right?
         * 
         * Have we decided if corridoes need energy or not?
         */
    }
    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }

    public override void Run()
    {
        base.Run();
    }
}
