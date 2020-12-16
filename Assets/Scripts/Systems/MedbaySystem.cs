using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private List<CrewMember> patients;
    private List<Room> rooms;
    private Room systemRoom;

    public MedbaySystem(List<Room> rooms)
    {
        SystemState = SystemState.IsOn;
        SystemType = SystemType.Medbay;
        patients = new List<CrewMember>();
        this.rooms = rooms;
        systemRoom = rooms.FirstOrDefault(x => x.RoomType == RoomType.MedBay);

        EnergyWanted = 0;
    }
    
    public void Run()
    {
        AirLevel = systemRoom.OxygenLevel;

        var patients = GetPatients(systemRoom);

        if (patients.Count > 0)
        {
            var doctor = GetDoctor(systemRoom);

            if (doctor != null)
            {
                TreatPatients(patients, doctor);
            }
        }
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

    public CrewMember GetDoctor(Room systemRoom)
    {
        return systemRoom.PresentCrewMembers.FirstOrDefault(x => x.crewData.profession == Profession.Scientist);
    }

    public List<CrewMember> GetPatients(Room systemRoom)
    {
        return systemRoom.PresentCrewMembers.Where(x => x.crewData.health < 100).ToList();
    }

    public void TreatPatients(List<CrewMember> patients, CrewMember doctor)
    {
        foreach (var patient in patients)
        {
            patient.crewData.health += SystemController.Instance.healingAmount;
            patient.crewData.health = Mathf.Clamp(patient.crewData.health, 0, 100);
        }
    }
}
