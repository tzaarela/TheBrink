using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorSystem : ShipSystem
{
    private float Fuel;
    private float FuelCost;
    //I might later "just" make this into a max for the float "Fuel" instead.
    private float FuelMax;
    private float Efficiency;
    private bool IsRetrograde;
    private float capacityLevel;

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }

    public ReactorSystem()
    {
        SystemType = SystemType.Reactor;
        SystemState = SystemState.IsOn;
        FuelCost = 1;
        Fuel = 100;
        FuelMax = 100;
        capacityLevel = 2;
        IsRetrograde = false;
    }

    public void BurnsFuel()
    {
        Fuel -= capacityLevel * FuelCost;
    }
    public void ProdEnergy()
    {

    }
    public void ProdSpeed()
    {

    }

    public void Run()
    {
        if(Fuel > 0)
        {
            BurnsFuel();
            ProdEnergy();
        }

        ProdSpeed();
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
/*
* So, when the game starts each system needs to be created.
* Each system will be so different that I will need to build them seperately
* However 
* After that the systems will only be run when online and on Update()...
* So are they run in Update here... but turned on and of through ShipController?
* (that would mean a lot of polling no? Is it better to have the systems tell if "on"?
* However I still don't see how that wouldn't result in "polling" hm...
* So, Systems are built here.
* Systems are updated here (when they run)
* But they are turned on and of through SystemController? Or through SystemTab?
* So each System will have a SystemType, but they will all be objects of the System-class?
* Yes, but they will also each have other aditional properties?
* 
* Well wait no.
* SystemController will say: "build me a system like this" to ShipSystem that will hold constructors for the different systems.
* ShipController will then turn them on and off.
* But ShipSystem should have the methods for each system... That are called from ShipControllers update right?
* ShipController has an update() like "RunSystems()" that goes through each system, and if it is on they run it?
* However that means that each system knows if it is on, but ShipController needs to ask them.
* Would be better if ShipController could look that up on its own maybe?
* 
* I will begin by building a ReactorSystem here.
* Then I can see if I want that to become it's own subclass, and have some overflow to other systems.
* They will definitely have things incommon, but I will decide what later.
* 
*/



/// <summary>
/// Calld from ShipController to create a reactor at game start, sets initial values.
/// </summary>
/// <param name="_systemType"></param>
/// <param name="_startingEfficiency"></param>
/// <param name="_startingFuel"></param>
//ShipSystem(SystemType _systemType, float _startingEfficiency, float _startingFuel, float _fuelConsumption, float _acceleration)
//{
//    //systemtype = _systemtype;

//    //efficiency = _startingefficiency;
//    //capacity = 0;

//    //storedfuel = _startingfuel;
//    //fuelconsumption = _fuelconsumption;

//    //acceleration = _acceleration;
//}

////Here we would need a bunch of methods right? So we are able to change all the things? Or will the values be manipulated from ShipController then?
////Let's do the simplest first, to collect more questions.

////TODO: Where should the method check if it has enough fuel? Here or in ShipController?
////TODO: Remember that fuel should not be able to go below zero, is so, it is set to zero.


//void RunReactor()
//{

//    if (ShipController.Instance.Ship.Fuel <= 0)
//    { //Maybe check this outside of this method instead?

//        Debug.Log("The reactor has run out of fuel!");
//        return;
//    }
//    else
//    {
//        ShipController.Instance.Ship.Fuel -= FuelConsumption * Capacity;
//        //Put into Ship later, that if below zero or above Max it resets.

//        ShipController.Instance.Ship.Capacitor += EnergyOutput * Capacity;
//        //Put into Ship later, that if below zero or above Max it resets.


//        if (!IsRetrograde)
//        {
//            ShipController.Instance.Ship.Speed += Acceleration * Capacity * Efficiency;
//        }
//        else
//        {
//            ShipController.Instance.Ship.Speed -= Acceleration * Capacity * Efficiency;
//        }



//        //Call on powerbank here
//        EnergyOutput = EnergyOutput * Capacity * Efficiency;
//    }

//    //sets acceleration * capacity * Efficiency
//    //checks if retrograde, if yes, * -1;
//    //Sets energyOutput * capacity * Efficiency
//    //Sends energy Output to "powerbank"
//    //Sends acceleration to Ship speed.
//}

//void Start()
//{

//}

//void Update()
//{

//}

/*
* This is the archetype
*/

//private float Efficiency { get; set; }
//private float Capacity { get; set; }

//private float StoredFuel { get; set; }
//private float FuelConsumption { get; set; }

//private float Acceleration { get; set; }

//private float EnergyOutput { get; set; }
