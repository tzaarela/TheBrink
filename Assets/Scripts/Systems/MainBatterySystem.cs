using Assets.Scripts.Rooms;
using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainBatterySystem : ShipSystem
{
    public bool isCharging;
    public bool isTargetLocked;

    public MainBatterySystem(List<Room> rooms)
    {
        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.MainBattery);

        PowerState = PowerState.IsOn;
        SystemType = SystemType.MainBattery;

        //Go through this later, also lift out some to the common list in top of SystemController.
        EnergyToMaintain = 0;
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

        EnergyWanted += EnergyToMaintain;

        /*
         * Okay, what do we want should happen here?
         * The system will consume a small amount of energy as long as it is turned on...
         * It will then ask if it has a targetlock...
         * So, it will be asked to lock on target... and then it will do so...
         * And at the same time it will begin to charge it's cannons...
         * 
         * First: Values for weapon charging,
         * value for locking on target
         * boolean isTargetLocked
         * boolean isWeaponCharged
         * If weapon is fired... so we need button here... then we send energyamountInShot on to the coming encounter, and have a method there that handles this.
        */


    }
}