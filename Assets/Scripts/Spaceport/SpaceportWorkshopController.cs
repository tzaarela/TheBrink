using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Spaceport;
using UnityEngine;
using TMPro;

public class SpaceportWorkshopController : SpaceportPanelController
{
    public static SpaceportWorkshopController instance;

    private RepairContent[] _repairsContent;
    [SerializeField] private RepairContent _selectAllRepairContent;
    [SerializeField] private FuelPanel _fuelPanel;

    [SerializeField] private TMP_Text _repairsCostText;
    [SerializeField] private TMP_Text _fuelCostText;
    [SerializeField] private TMP_Text _totalCostText;

    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        base.Awake();
        
        _tabType = SpaceportTabType.Workshop;

        GetAllComponents();
    }

    protected override void Start()
    {
        base.Start();

        Init();
    }

    protected override void GetAllComponents()
    {
        base.GetAllComponents();

        Transform repairPanelTransform = _panels[(int) WorkshopPanelType.Repair].transform;
        _repairsContent = new RepairContent[repairPanelTransform.childCount];
        for (int i = 0; i < _repairsContent.Length; i++)
        {
            _repairsContent[i] = repairPanelTransform.GetChild(i).GetComponent<RepairContent>();
        }
    }

    private void Init()
    {
        foreach (RepairContent repairContent in _repairsContent)
        {
            repairContent.Init();
        }
        
        _selectAllRepairContent.Init("Select All", GetAllRoomCost(false));
        
        UpdateTotalRepairCostUI();
        UpdateTotalFuelCostUI();
        UpdateTotalCostUI();
    }

    public void UpdateTotalRepairCostUI()
    {
        int cost = GetAllRoomCost(true);
        _repairsCostText.text = $"${-cost}";
        UpdateTotalCostUI();
    }

    public void UpdateTotalFuelCostUI()
    {
        int cost = _fuelPanel.GetTotalFuelCost();
        _fuelCostText.text = $"${-cost}";
        UpdateTotalCostUI();
    }

    public void UpdateTotalCostUI()
    {
        int cost = GetAllRoomCost(true) + _fuelPanel.GetTotalFuelCost();
        _totalCostText.text = $"${-cost}";
    }

    private int GetAllRoomCost(bool selected)
    {
        int cost = 0;

        foreach (RepairContent repairContent in _repairsContent)
        {
            if (selected)
                cost += repairContent.IsSelected ? repairContent.GetCost() : 0;
            else
                cost += repairContent.GetCost();
        }
        
        return cost;
    }
    
    public void ToggleSelectAll()
    {
        _selectAllRepairContent.ToggleSelectedObject();

        SetAllSelectedObjects(_selectAllRepairContent.IsSelected);

        UpdateTotalRepairCostUI();
    }

    private void SetAllSelectedObjects(bool active)
    {
        foreach (RepairContent repairContent in _repairsContent)
        {
            repairContent.SetSelectedObject(active);
        }
        _selectAllRepairContent.SetSelectedObject(active);
    }

    public void ConfirmPayment()
    {
        int totalCost = GetAllRoomCost(true) + _fuelPanel.GetTotalFuelCost();
        
        if (GameController.Instance.ship.cash < totalCost)
            return;
        
        SpaceportUIController.instance.UpdateCash(-totalCost);
        HealSelectedRooms();
        _selectAllRepairContent.UpdateCostText(GetAllRoomCost(false));
        SetAllSelectedObjects(false);
        UpdateTotalRepairCostUI();
        
        _fuelPanel.UpdateFuelUI(true);
        UpdateTotalFuelCostUI();
        
        UpdateTotalCostUI();
    }

    private void HealSelectedRooms()
    {
        foreach (RepairContent repairContent in _repairsContent)
        {
            if (repairContent.IsSelected)
                 repairContent.SetFullHealth();
        }
    }
}
