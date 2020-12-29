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
    public bool isLockedOnTarget;
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

        if (isCharging)
        {
            ChargingWeapons();       
        }

        if (isLockingOnTarget)
        {
            LockingOnTarget();
        }
    }

    public void ChargingWeapons()
    {
        if (weaponCharge >= maxWeaponCharge)
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

    public void LockingOnTarget()
    {
        if (probOfHittingTarget < 100)
        {
            probOfHittingTarget++;
        }
        else
        {
            isLockedOnTarget = true;
            isLockingOnTarget = false;
            Debug.Log("Target lock aquired.");

        }
    }
    public void LockOn()
    {
        if (isLockingOnTarget == false)
        {
            isLockingOnTarget = true;
        }
        else
        {
            isLockingOnTarget = false;
            isLockedOnTarget = false;
        }
    }

    public void Charge()
    {
        if (isCharging == false)
        {
            isCharging = true;
        }
        else
        {
            isCharging = true;
        }
    }

    public void Fire()
    {

        Debug.Log("The weapons fire!");

        didHitTarget = CheckForHit(probOfHittingTarget);

        if (didHitTarget)
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

        didHitTarget = false;
        isCharged = false;
        isLockedOnTarget = false;
    }

    public bool CheckForHit(float probOfHittingTarget)
    {
        int random = (Random.Range(0, 101));

        if (probOfHittingTarget < random)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }
}