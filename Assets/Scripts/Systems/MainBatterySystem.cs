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

    public void Charge()
    {
        if(weaponCharge < maxWeaponCharge)
        {
            weaponCharge++;
            //TODO, so here we want to decrease energy, or give this system some way of sipponing energy from other systems.
        }else
        {
            isCharged = true;
        }
    }

    public void Fire()
    {

        Debug.Log("The weapons fire!");
        
        didHitTarget = CheckForHit(probOfHittingTarget);

        if(didHitTarget)
        {
            Debug.Log("The weapons hit their target!");
        }
        else
        {
            Debug.Log("The weapons missed their target.");
            isCharging = true;
            isLockingOnTarget = true;
        }

        weaponCharge = 0;
        probOfHittingTarget = 0;
        isCharged = false;
    }

    public bool CheckForHit(float probOfHittingTarget)
    {
        int random = (Random.Range(0, 101));

        if(probOfHittingTarget < random)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }

    public void LockOn()
    {
        if (probOfHittingTarget < 100)
        {
            probOfHittingTarget++;
        }
        else
        { 
            isTargetLocked = true;
            isLockingOnTarget = false;
            Debug.Log("Target Lock aquired.");
            //I prob want to add later here that the button changes when the target is locked, when you have a hundred per cent chance.
            //And I need this to be set to zero when you fire, and also if you detoggle it right?
        }
    }

    //public bool Charging()
    //{

    //}
}