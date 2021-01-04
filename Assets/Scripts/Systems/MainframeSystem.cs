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
        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.MainFrame);

        SystemType = SystemType.Mainframe;

        UpkeepCost = SystemController.Instance.mainFrameUpkeepSystem;

        CurrentEnergy = 50;
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
            if(system.PowerState == PowerState.IsOn)
            {
                if (ship.capacitor >= SystemController.Instance.energyPortion)
                {
                    //TODO: add a max here, so that it doesn't just keep puring energy into a system.
                    system.CurrentEnergy += SystemController.Instance.energyPortion;
                    ship.capacitor -= SystemController.Instance.energyPortion;
                }
            }
        }
    }
}
