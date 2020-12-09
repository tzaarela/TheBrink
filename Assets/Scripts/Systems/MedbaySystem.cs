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

    public MedbaySystem()
    {
        SystemState = SystemState.IsOn;
    }

    public void Run()
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

    //public void UpdateMedbay()
    //{
    //    hasDoctor = CheckForDoctor();

    //    hasPatient = CheckForPatient();

    //    if (hasDoctor == true && hasPatient == true)
    //    {
    //        TreatPatient();
    //    }

    //}

    ////public bool CheckForDoctor()
    //{
    //    //Checks if doctor is in the room and unoccupied
    //}

    //public bool CheckForPatient()
    //{
    //    //Checks if there is other CrewMember there, and IF that CrewMember is hurt.
    //}

    //public void TreatPatient()
    //{
    //    //Take crewMembers health, increase it by Doctors skill (but not above top value).
    //}
}
