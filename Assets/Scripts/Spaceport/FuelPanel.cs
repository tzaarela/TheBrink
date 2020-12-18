using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelPanel : MonoBehaviour
{
    [SerializeField] private FuelContent[] _fuelBars;
    
    [SerializeField] private TMP_Text _fuelText;
    [SerializeField] private TMP_Text _distanceText;
    
    private const int MaxFuelDistance = 10000;
    private const int MinFuelModifier = 10;
    private const int CashPerFuel = 10;

    private float _modifiedFuel;

    private float _holdTimer;
    private const float MaxHoldTime = 0.05f;
    private const float HoldTimeStartDelay = 0.3f;
    private int holdCounter; // TODO Make holding down count faster with time.
    private bool _holdButton;
    private bool _refuel;

    private void Awake()
    {
    }

    private void Start()
    {
        Init();
    }

    public void Update()
    {
        if (!_holdButton)
            return;

        _holdTimer += Time.deltaTime;
        if (_holdTimer >= MaxHoldTime)
        {
            _holdTimer = 0f;
            if (_refuel)
                Refuel();
            else
                Defuel();
        }
    }

    private void Init()
    {
        _modifiedFuel = GameController.Instance.ship.fuel;
        UpdateFuelUI(true);
    }

    public void UpdateFuelUI(bool applyChanges)
    {
        if (applyChanges)
            GameController.Instance.ship.fuel = _modifiedFuel;
        
        
    
        int fuelPercentInt = Mathf.CeilToInt(GameController.Instance.ship.GetFuelPercent() * 100);
        float modifiedPercent = _modifiedFuel / GameController.Instance.ship.maxFuel;
        int modifiedPercentInt = Mathf.CeilToInt(modifiedPercent * 100);
        
        Debug.Log($"fuelPercentInt: {fuelPercentInt}\n" +
                            $"modifiedPercent: {modifiedPercent}\n" +
                            $"modifiedPercentInt: {modifiedPercentInt}");
        
        _fuelText.text = $"{fuelPercentInt}%\n({modifiedPercentInt}%)";
        _distanceText.text = $"{Mathf.CeilToInt(GameController.Instance.ship.GetFuelPercent() * MaxFuelDistance)} light years\n({modifiedPercent * MaxFuelDistance})";
        
        modifiedPercent *= 18f;
        foreach (FuelContent fuelContent in _fuelBars)
        {
            fuelContent.UpdateUI(Mathf.Clamp(modifiedPercent, 0f, 1f), applyChanges);
            modifiedPercent -= 1f;
        }
        
        SpaceportWorkshopController.instance.UpdateTotalFuelCostUI();
    }

    public void Refuel()
    {
        _modifiedFuel = Mathf.Clamp(_modifiedFuel + MinFuelModifier, 0f, GameController.Instance.ship.maxFuel);
        UpdateFuelUI(false);
    }
    
    public void StartHold(bool refuel)
    {
        _holdButton = true;
        _holdTimer = -(HoldTimeStartDelay - MaxHoldTime);
        _refuel = refuel;
    }

    public void Defuel()
    {
        _modifiedFuel = Mathf.Clamp(_modifiedFuel - MinFuelModifier, 0f, GameController.Instance.ship.maxFuel);
        UpdateFuelUI(false);
    }

    public void StopHold()
    {
        _holdButton = false;
    }

    public int GetTotalFuelCost()
    {
        return Mathf.CeilToInt(_modifiedFuel - GameController.Instance.ship.fuel) * CashPerFuel;
    }
}
