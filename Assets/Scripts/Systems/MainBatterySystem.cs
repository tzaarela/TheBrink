using Assets.Scripts.Rooms;
using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainBatterySystem : ShipSystem
{
    public bool isCharging;
    public bool isCharged;
    public bool isLockingOnTarget;
    public bool isTargetLocked;
    public bool didHitTarget;
    public bool isFiring;

    public float weaponCharge;
    public float probOfHittingTarget;
    public float maxWeaponCharge;

    public MainBatterySystem(List<Room> rooms)
    {
        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.MainBattery);

        PowerState = PowerState.IsOn;
        SystemType = SystemType.MainBattery;

        //TODO: Check, are bools always initially set to false?

        //TODO: Clamp values, also, is there easier way to represent percentages?

        //TODO: Se what values here that you might want to lift out to SystemController.

        //Go through this later, also lift out some to the common list in top of SystemController.

        EnergyToMaintain = 0;
        EnergyWanted = 0;

        weaponCharge = 0;
        probOfHittingTarget = 0;
        maxWeaponCharge = 0;
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

        //isCharging toggles from button.

        //isLockingOn toggles from button.

        if(isFiring)
        {
            //checks here random against prob to hit. and sets is hit after that?
            //if true then set energy to zero, send that on, also check for how much energy was used?

            //sets isCharged to false, sets isCharging to true, sets TargetLock to false
        }

        //Should this be its own method?
        if(isLockingOnTarget)
        {
            if(probOfHittingTarget >= 100)
            {
                isTargetLocked = true;
                isLockingOnTarget = false;
            }
            probOfHittingTarget++;
        }

        if(isCharging)
        {
            if(weaponCharge >= maxWeaponCharge)
            {
                weaponCharge = maxWeaponCharge;
                
                isCharging = false;
                isCharged = true;
            }
            else
            {
                weaponCharge++;
            }
        }

        
        //Method for charging weapons...
        //If weapons are charged and you turn of the weapons you lose that charge right?
        //Method for locking on to target...
        //Method for increasing probability on managing to hitting target.
        
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