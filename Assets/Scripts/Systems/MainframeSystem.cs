using Assets.Scripts.Rooms;
using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainframeSystem : ShipSystem
{
    Ship ship;

    public MainframeSystem(Ship ship, List<Room> rooms)
    {
        this.ship = ship;
        SystemRoom = rooms.FirstOrDefault(x => x.SystemType == SystemType.Mainframe);

        SystemType = SystemType.Mainframe;

        UpkeepCost = SystemController.Instance.mainFrameUpkeepSystem;

        CurrentEnergy = 50;

        MaxEnergy = 100;
    }

    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }

    public override void Run()
    {
        var systems = SystemController.Instance.ShipSystems;
        //TODO: Check this with Saarela, should it really be set up here?

        foreach (var system in systems)
        {
            //Checks if the system is on before giving it energy
            if (system.PowerState == PowerState.IsOn && !system.SystemRoom.hasElectricFailure)
            {
                //Checks if the ship has energy to give AND if the system needs energy, before giving energy.
                if (ship.capacitor >= SystemController.Instance.energyPortion &&
                    system.CurrentEnergy + SystemController.Instance.energyPortion < system.MaxEnergy)
                {
                    system.CurrentEnergy += SystemController.Instance.energyPortion;
                    ship.capacitor -= SystemController.Instance.energyPortion;
                }
            }
        }
    }
}
