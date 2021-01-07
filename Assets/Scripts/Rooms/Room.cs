using Assets.Scripts;
using Assets.Scripts.Crew;
using Assets.Scripts.InterfacePanels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Rooms
{
    public class Room : UITrigger
    {
        public float RadiationLevel { get => radiationLevel; set => radiationLevel = value; }

        public SystemType SystemType;
        public RoomState RoomState;

        public List<CrewMember> PresentCrewMembers { get; set; }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (isSelected)
                    onRoomSelected.Invoke();
                else
                    onRoomDeselected.Invoke();
            }
        }
        public float OxygenLevel
        {
            get {return oxygenLevel; }
            set
            {
                oxygenLevel = Mathf.Clamp(value, 0, 100);
            }
        }
        
        public List<Hazard> Hazards { get; set; }
        public bool hasElectricFailure;

        private Door _door;
        private UnityEvent onRoomSelected;
        private UnityEvent onRoomDeselected;

        [SerializeField]
        private Transform highlight;
        [SerializeField]
        private WarningHighlight warningHighlight;
        [SerializeField]
        public List<Waypoint> waypoints;
        [SerializeField]
        private float radiationLevel;
        [SerializeField]
        private float oxygenLevel;
        [SerializeField]
        private float roomHealth;

        public RoomData data;

        public void Awake()
        {
            SystemType = data.systemType;
            OxygenLevel = 100;
            RoomState = RoomState.Open;
            Hazards = new List<Hazard>();
            PresentCrewMembers = new List<CrewMember>();
            
            Highlight(false);


            if (onRoomSelected == null)
                onRoomSelected = new UnityEvent();

            if (onRoomDeselected == null)
                onRoomDeselected = new UnityEvent();

            onRoomSelected.AddListener(OnRoomSelected);
            onRoomDeselected.AddListener(OnRoomDeselected);
        }

        private void OnRoomSelected()
        {
            highlight.gameObject.SetActive(true);
        }

        private void OnRoomDeselected()
        {
            highlight.gameObject.SetActive(false);
        }

        public void CreateHazard(HazardType _hazardType, float _severityAmount)
        {
            Hazard hazard = new Hazard(_hazardType, _severityAmount, this);

            Hazards.Add(hazard);
        }

        public void UpdateHazard()
        {
            Hazards.RemoveAll(x => x.IsFinished);


            var hasHazard = Hazards.Count > 0;

            warningHighlight.gameObject.SetActive(hasHazard || OxygenLevel <= 0);

            if(hasHazard || OxygenLevel <= 0)
                SetWarningIcons();

            if (OxygenLevel <= 0)
                warningHighlight.oxygenIcon.SetActive(true);
            else
                warningHighlight.oxygenIcon.SetActive(false);

            foreach (Hazard hazard in Hazards)
            {
                hazard.UpdateHazard();
            }
        }

        public void SetWarningIcons()
        {
            if (Hazards.Any(x => x.HazardType == HazardType.Fire))
                warningHighlight.fireIcon.SetActive(true);
            else
                warningHighlight.fireIcon.SetActive(false);

            if (Hazards.Any(x => x.HazardType == HazardType.Breach))
                warningHighlight.breachIcon.SetActive(true);
            else
                warningHighlight.breachIcon.SetActive(false);

            if (Hazards.Any(x => x.HazardType == HazardType.ElectricFailure))
                warningHighlight.electricIcon.SetActive(true);
            else
                warningHighlight.electricIcon.SetActive(false);

            if (OxygenLevel <= 0)
                warningHighlight.oxygenIcon.SetActive(true);
            else
                warningHighlight.oxygenIcon.SetActive(false);
        }

        public void AirDrain(float drainLevel)
        {
            OxygenLevel -= drainLevel;
            if (OxygenLevel <= 0)
                PresentCrewMembers.ForEach(x => x.TakeDamage(1f));
        }

        public List<Command> GetAvailableCommandsForRoom(CrewMember crewMember)
        {
            //TODO - GET CORRECT TASKS FOR ROOM AND CHARACTER
            List<Command> availableTasks = new List<Command>();

            if(crewMember.CurrentWayPoint != waypoints[0])
                availableTasks.Add(new MoveCommand(crewMember, this));

            if (Hazards.Any(x => x.HazardType == HazardType.Fire))
            {
                availableTasks.Add(new ExtinguishFireCommand(crewMember, this));
            }

            if (Hazards.Any(x => x.HazardType == HazardType.Breach))
            {
                availableTasks.Add(new FixHullBreachCommand(crewMember, this));
            }

            if (Hazards.Any(x => x.HazardType == HazardType.ElectricFailure))
            {
                availableTasks.Add(new FixElectricCommand(crewMember, this));
            }

            return availableTasks;
        }

        public void ExtinguishFire(CrewMember crewMember)
        {
            Hazard hazardToFix;
            var fires = Hazards.Where(x => x.HazardType == HazardType.Fire).ToList();

            if (fires.Count <= 0)
            {
                ConsoleController.instance.PrintToConsole($"{crewMember.name}: I've extinguished the fire in {crewMember.CurrentCommand.Destination.name}. ", 0.01f, true);
                return;
            }
            else
            {
                hazardToFix = fires[0];
            }
            
            hazardToFix.SeverityAmount -= crewMember.crewData.extinguishFireSkill;

            if (hazardToFix.SeverityAmount <= 0)
            {
                Hazards.Remove(hazardToFix);
            }
        }

        public void FixElectricFailure(CrewMember crewMember)
        {
            Hazard hazardToFix;
            var electricFailures = Hazards.Where(x => x.HazardType == HazardType.ElectricFailure).ToList();

            if(electricFailures.Count <= 0)
            {
                ConsoleController.instance.PrintToConsole($"{crewMember.name}: I've fixed the electric failure in {crewMember.CurrentCommand.Destination.name}. ", 0.01f, true);
                return;
            }
            else
            {
                hazardToFix = electricFailures[0];
            }

            hazardToFix.SeverityAmount -= crewMember.crewData.fixElectricFailureSkill;

            if (hazardToFix.SeverityAmount <= 0)
            {
                Hazards.Remove(hazardToFix);
            }
        }

        public void FixHullBreach(CrewMember crewMember)
        {
            Hazard hazardToFix;
            var breaches = Hazards.Where(x => x.HazardType == HazardType.Breach).ToList();

            if (breaches.Count <= 0)
            {
                ConsoleController.instance.PrintToConsole($"{crewMember.name}: I've repaired the hullbreach in {crewMember.CurrentCommand.Destination.name}. ", 0.01f, true);
                return;
            }
            else
            {
                hazardToFix = breaches[0];
            }

            hazardToFix.SeverityAmount -= crewMember.crewData.fixHullBreachSkill;

            if (hazardToFix.SeverityAmount <= 0)
            {
                Hazards.Remove(hazardToFix);
            }
        }

        public IShipSystem GetShipSystem()
        {
            return SystemController.Instance.GetSystemOfType(SystemType);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            Highlight(true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            Highlight(false);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            //if (Input.GetMouseButtonDown(0) && !ContextMenuController.instance.IsOpen)
            //{
            //    ContextMenuController.instance.CloseContextMenu();
            //}

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                //RoomController.Instance.Rooms.FirstOrDefault(x => x.IsSelected == true).IsSelected = false;
                //IsSelected = true;

                if (CrewController.Instance.GetSelectedCrewMember() != null && SystemType != SystemType.Corridors)
                {
                    var availableTasks = GetAvailableCommandsForRoom(CrewController.Instance.GetSelectedCrewMember());
                    ContextMenuController.instance.OpenContextMenu(availableTasks);
                }
            }
        }
        public void Highlight(bool isOn)
        {
            if(isOn)
                highlight.gameObject.SetActive(true);
            else
                highlight.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                var crewMember = collision.GetComponent<CrewMember>();
                PresentCrewMembers.Add(crewMember);

                //Vector3[] positions = new Vector3[]
                //{
                //    new Vector3(0,0,0),
                //    new Vector3(10,0,0),
                //    new Vector3(-10,0,0),
                //};

                //for (int i = 0; i < PresentCrewMembers.Count; i++)
                //{
                    
                //    PresentCrewMembers[i].transform.position += positions[i];
                //}
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                var crewMember = collision.GetComponent<CrewMember>();
                PresentCrewMembers.Remove(crewMember);
            }
        }
    }
}

