using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSystem : ShipSystem
{
    //Navigationvariables
    float DistanceToStarport;
    float DistanceToRetrogradeBurn;
    float DisanceToNextEncounter;

    //Time variables
    float EstimatedTimetoArrival;
    float TimeUntilRetrogradeBurn;
    float TimeUntilNextEncounter;

    BridgeSystem()
    {
        //Needs to get info when Route is selected here
        //But when?
        //MissionController gives info when route has been selected, to BridgeSystem.
    }

    public void BridgeUpdate()
    {
        //Check if Docking? But that should be other system surely?

        //UpdateETA,

        //Figure out, should this be sent somewhere else, or should it just be set as field here right?

        //UpdateTimeToRetro

        //UpdateTimeToEncounter
    }

    public void UpdateETA()
    {

    }

    public void UpdateTimeToRetro()
    {

    }

    public void UpdateTimeToEncounter()
    {

    }

}
