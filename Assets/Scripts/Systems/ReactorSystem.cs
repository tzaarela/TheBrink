using Assets.Scripts;
using Assets.Scripts.Rooms;
using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReactorSystem : ShipSystem
{
    private Ship ship;

    public float fuelCost;

    public float Efficiency
    {
        get { return _efficiency; }
        set { _efficiency = Mathf.Clamp(value, 0.00f, 1.00f); }
    }
    private float _efficiency;

    public int CapacityLevel
    {
        get { return _capacityLevel; }
        set { _capacityLevel = Mathf.Clamp(value, 1, 6); }
    }
    private int _capacityLevel;

    public float EnergyOutput { get; set; }

    public float EnergyProduction;


    public ReactorSystem(Ship ship, List<Room> rooms)
    {
        this.ship = ship;

        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.Reactor);
        SystemType = SystemType.Reactor;
        PowerState = PowerState.IsOn;

        fuelCost = SystemController.Instance.fuelCost;
        CapacityLevel = SystemController.Instance.capacityLevel;
        
        EnergyProduction = SystemController.Instance.energyProduction;
        UpkeepCost = SystemController.Instance.reactorUpkeepSystem;

        //This is my ugly way of trying to make sure that this system does not drain energy from other systems.
        CurrentEnergy = 100;

        //Removed this for now, so we won't have that annoying notice.
        //IsRetrograde = false;

        Efficiency = 1.00f;
    }

    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }
    
    public override void Run()
    {

        if (ship.fuel > 0)
        {
            //TODO: I think JS talked about this being a bit "stiff" I might wish that the methods sends things into each other instead?
            BurnsFuel();
            ProdEnergy();
            ProdSpeed();
        }
        else
        {
            ConsoleController.instance.PrintToConsole("Reactor has no more fuel");
            //TODO: Remove this later, if you implement the other way for the ship speed and travel and momentum to happen.
            ship.speed = 0;
        }

    }

    public void BurnsFuel()
    {
        ship.fuel = Mathf.Clamp(ship.fuel - CapacityLevel * fuelCost, 0, ship.maxFuel);
    }

    public void ProdEnergy()
    {
        EnergyOutput = EnergyProduction * CapacityLevel * Efficiency;

        ship.capacitor = Mathf.Clamp(EnergyOutput + ship.capacitor, 0, ship.maxCapacitor);
    }

    public void ProdSpeed()
    {
        ship.speed = CapacityLevel * 2;
        //TODO: Make it so that this isn't a magic number.
    }
}
