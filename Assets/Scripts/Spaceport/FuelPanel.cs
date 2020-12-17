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
    
        int fuelPercentInt = Mathf.CeilToInt(GameController.Instance.ship.GetFuelPercent() * 100);
        float modifiedPercent = _modifiedFuel / GameController.Instance.ship.maxFuel;
        int modifiedPercentInt = Mathf.CeilToInt(modifiedPercent * 100);
        
        _fuelText.text = $"{fuelPercentInt}%\n({modifiedPercentInt}%)";
        _distanceText.text = $"{fuelPercentInt * MaxFuelDistance} light years\n({modifiedPercentInt * MaxFuelDistance})";
        
        modifiedPercent *= 10f;
        foreach (FuelContent fuelContent in _fuelBars)
        {
            fuelContent.UpdateUI(Mathf.Clamp(modifiedPercent, 0f, 1f), applyChanges);
            modifiedPercent -= 1f;
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
