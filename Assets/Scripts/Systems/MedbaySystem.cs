using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Systems;

public class MedbaySystem : ShipSystem
{
    public bool hasDoctor;
    public bool hasPatient;

    public Queue<CrewMember> patientsToTreat;

    private List<CrewMember> patients;
    private List<Room> rooms;

    public MedbaySystem(List<Room> rooms)
    {
        PowerState = PowerState.IsOn;
        SystemType = SystemType.Medbay;
        patients = new List<CrewMember>();
        this.rooms = rooms;
        SystemRoom = rooms.FirstOrDefault(x => x.RoomType == SystemType.MedBay);

        UpkeepCost = SystemController.Instance.medbayUpkeepSystem;
        CurrentEnergy = 50;

    }

    public override void Update()
    {
        AirLevel = SystemRoom.OxygenLevel;
        base.Update();
    }

    public override void Run()
    {

        var patients = GetPatients(SystemRoom);

        if (patients.Count > 0)
        {
            var doctor = GetDoctor(SystemRoom);

            if (doctor != null)
            {
                TreatPatients(patients, doctor);
            }
        }
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
            patient.crewData.health += SystemController.Instance.healingAmount * Capacity;
            patient.crewData.health = Mathf.Clamp(patient.crewData.health, 0, 100);
        }
    }
}
