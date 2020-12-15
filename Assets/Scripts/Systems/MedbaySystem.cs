using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbaySystem : ShipSystem
{
    public bool hasDoctor;
    public bool hasPatient;

    public Queue<CrewMember> patientsToTreat;

    public SystemType SystemType { get; set; }
    public SystemState SystemState { get; set; }
    
    public float EnergyWanted { get; set; }
    public float CurrentEnergy { get; set; }
    public float EnergyToMaintain { get; set; }
    
    public float AirLevel { get; set; }

    public MedbaySystem()
    {
        SystemState = SystemState.IsOn;
        SystemType = SystemType.Medbay;

        EnergyWanted = 0;
    }
    
    public void Run()
    {


        if(hasPatient == true)
        {
            hasDoctor = CheckForDoctor();

            if (hasDoctor == true)
            { 

            }
    }

        throw new System.NotImplementedException();
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

    public void UpdateMedbay()
    {
        hasDoctor = CheckForDoctor();

        hasPatient = CheckForPatient();

        if (hasDoctor == true && hasPatient == true)
        {
            TreatPatient();
        }

    }

    public bool CheckForDoctor()

    {
        return true;
        //checks if doctor is in the room and unoccupied
    }

    public bool CheckForPatient()
    {
        return true;
    //checks if there is other crewmember there, and if that crewmember is hurt.
    }

    public void TreatPatient()
    {
    //take crewmembers health, increase it by doctors skill (but not above top value).
    }
}
