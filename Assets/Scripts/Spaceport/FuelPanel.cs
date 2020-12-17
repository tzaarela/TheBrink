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
    private const int MinFuelModifier = 20;
    private const int CashPerFuel = 10;

    private float _modifiedFuel;

    private void Awake()
    {
    }

    private void Start()
    {
        Init();
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
    
        // float fuelPercent = GameController.Instance.ship.GetFuelPercent();
        float fuelPercent = _modifiedFuel / GameController.Instance.ship.maxFuel;
        _fuelText.text = Mathf.CeilToInt(fuelPercent * 100) + "%";
        _distanceText.text = $"{Mathf.CeilToInt(fuelPercent * MaxFuelDistance)} light years";
        
        fuelPercent *= 10f;
        foreach (FuelContent fuelContent in _fuelBars)
        {
            fuelContent.UpdateUI(Mathf.Clamp(fuelPercent, 0f, 1f), applyChanges);
            fuelPercent -= 1f;
        }
        
        SpaceportWorkshopController.instance.UpdateTotalFuelCostUI();
    }

    public void Refuel()
    {
        // GameController.Instance.ship.ModifyFuel(true);
        _modifiedFuel = Mathf.Clamp(_modifiedFuel + MinFuelModifier, 0f, GameController.Instance.ship.maxFuel);
        UpdateFuelUI(false);
    }

    public void Defuel()
    {
        // GameController.Instance.ship.ModifyFuel(false);
        _modifiedFuel = Mathf.Clamp(_modifiedFuel - MinFuelModifier, 0f, GameController.Instance.ship.maxFuel);
        UpdateFuelUI(false);
    }

    public int GetTotalFuelCost()
    {
        return Mathf.CeilToInt(_modifiedFuel - GameController.Instance.ship.fuel) * CashPerFuel;
    }
}
