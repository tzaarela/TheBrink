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
        set { _capacityLevel = Mathf.Clamp(value, 1, 3); }
    }
    private int _capacityLevel;

    public float EnergyOutput { get; set; }
    public SystemType SystemType { get; set; }
    public PowerState PowerState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float CurrentEnergyInSystem { get; set; }
    public float EnergyToMaintain { get; set; }
    public float AirLevel { get; set; }

    private Room systemRoom;

    public ReactorSystem(Ship ship, List<Room> rooms)
    {
        this.ship = ship;


        systemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.Reactor);
        SystemType = SystemType.Reactor;
        PowerState = PowerState.IsOn;
        fuelCost = SystemController.Instance.fuelCost;
        CapacityLevel = SystemController.Instance.capacityLevel;
        //Removed this for now, so we won't have that annoying notice.
        //IsRetrograde = false;
        
        //Wait, surely this is bizzarre? What was I thinking? Having an energyoutput that is LOWER than the bottleneck?
        EnergyOutput = ship.capacitorBottleNeck / 3;
        Efficiency = 1.00f;

        EnergyWanted = 0;
    }

    public void Update()
    {
        AirLevel = systemRoom.OxygenLevel;
    }
    
    public void Run()
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

        CurrentEnergyInSystem = CurrentEnergy;

    }

    public void BurnsFuel()
    {
        ship.fuel = Mathf.Clamp(ship.fuel - CapacityLevel * fuelCost, 0, ship.maxFuel);
    }

    public void ProdEnergy()
    {
        EnergyOutput = CapacityLevel * Efficiency;

        ship.capacitor = Mathf.Clamp(EnergyOutput + ship.capacitor, 0, ship.maxCapacitor);
    }

    public void ProdSpeed()
    {
        ship.speed = CapacityLevel;
    }

    public void SetEnergyWanted()
    {
        throw new System.NotImplementedException();
    }
    
    public void Reboot()
    {
        throw new System.NotImplementedException();
    }

    public void RunDiagnostic()
    {
        throw new System.NotImplementedException();
    }
}
