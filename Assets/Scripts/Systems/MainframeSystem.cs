using Assets.Scripts.Rooms;
using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainframeSystem : ShipSystem
{
    Ship ship;

    //public float TotalEnergyNeeded { get; set; }

    public MainframeSystem(Ship ship, List<Room> rooms)
    {
        this.ship = ship;
        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.MainFrame);

        SystemType = SystemType.Mainframe;

        EnergyWanted = 0;
    }

    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }

    public override void Run()
    {
        var systems = SystemController.Instance.ShipSystems;

        foreach (var system in systems)
        {
            if(system.PowerState == PowerState.IsOn)
            {
                if (ship.capacitor >= SystemController.Instance.energyPortion)
                {
                    system.CurrentEnergy += SystemController.Instance.energyPortion;
                    ship.capacitor -= SystemController.Instance.energyPortion;
                }
            }
        }

        //var energyNeeded = GetEnergyNeeded();

        //if (energyNeeded > 0)
        //{
        //    var energyFragment = DivideEnergy(energyNeeded);

        //    SendEnergyOut(energyFragment);
        //}
    }
    /// <summary>
    /// Goes through all active shipsystems and gathers energyWanted into a float
    /// </summary>
    /// <returns>A float of energyNeeded from all active systems</returns>
    //public float GetEnergyNeeded()
    //{
    //    TotalEnergyNeeded = 0;

    //    var systems = SystemController.Instance.ShipSystems;

    //    foreach (var system in systems)
    //    {
    //        system.CurrentEnergy = 0;
            
    //        if(system.PowerState == PowerState.IsOn)
    //        { 
    //            TotalEnergyNeeded += system.EnergyWanted;
    //        }
    //    }
    //    return TotalEnergyNeeded;
    //}
    /// <summary>
    /// Takes in float energyNeeded, decreases the ship Capacitor & increases MainframeSystem.CurrentEnergy
    /// in a while loop until it has enough energy OR has hit the Ship.CapacitorBottleNeck OR ship.Capacitor is zero.
    /// </summary>
    /// <param name="energyNeeded"></param>
    /// <returns>CurrentEnergy divided by energyNeeded</returns>
    //public float DivideEnergy(float energyNeeded)
    //{
    //    while(energyNeeded >= ship.capacitorBottleNeck && ship.capacitor > 0)
    //    {
    //        CurrentEnergy++;
    //        ship.capacitor--;
    //    }

    //    return CurrentEnergy / energyNeeded;
    //}
    /// <summary>
    /// Goes through all active systems, and sets their CurrentEnergy =
    /// energyFragment mult * EnergyWanted of that system.
    /// This means that each system will receive energy proportional to their need.
    /// </summary>
    /// <param name="energyFragment"></param>
    //public void SendEnergyOut(float energyFragment)
    //{
    //    var activeSystems = SystemController.Instance.GetActiveSystems();

    //    foreach (var activeSystem in activeSystems)
    //    {
    //        activeSystem.CurrentEnergy = energyFragment * activeSystem.EnergyWanted;
    //    }
    //}
}
