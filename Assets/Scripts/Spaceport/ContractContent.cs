using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContractContent : MonoBehaviour
{
    [Header("Contract Details")]
    [SerializeField] private TMP_Text _contractName;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _difficulty;
    [SerializeField] private TMP_Text _difficultyDescription;
    [SerializeField] private TMP_Text _fuelRequirements;
    [SerializeField] private TMP_Text _employeRequirements;
    [SerializeField] private TMP_Text _earnings;
    [SerializeField] private GameObject[] _buttons;

    [Header("Galaxy Map")]
    [SerializeField] private TMP_Text _threatPercent;
    [SerializeField] private TMP_Text _alienLevel;
    [SerializeField] private TMP_Text _pirateLevel;
    [SerializeField] private TMP_Text _routeLength;
    [SerializeField] private TMP_Text _routeTime;

    [Header("Text Colors")]
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _errorColor;

    private MissionContract _contract;

    public void SetupContract(MissionContract contract, bool accepted)
    {
        _contract = contract;
        
        _contractName.text = contract.contractName;
        _description.text = contract.description;
        _difficulty.text = contract.difficulty;
        _difficultyDescription.text = contract.difficultyDescription;

        _fuelRequirements.text = $"Average fuel required: {contract.averageFuelPercentNeeded}%";
        if (Mathf.CeilToInt(GameController.Instance.ship.GetFuelPercent() * 100) >= contract.averageFuelPercentNeeded)
            _fuelRequirements.color = _normalColor;
        else
            _fuelRequirements.color = _errorColor;
        
        _employeRequirements.text = $"Crew members required: {contract.crewMembersNeeded}";
        if (GameController.Instance.ship.GetCrewCount() >= contract.crewMembersNeeded)
            _employeRequirements.color = _normalColor;
        else
            _employeRequirements.color = _errorColor;

        _earnings.text = $"Credit Earnings: ${contract.cashEarnings.ToString("N0")}\n" +
                         $"Bonus Earnings: ${contract.bonusEarnings.ToString("N0")}";

        _threatPercent.text = $"{contract.threatPercent}%";
        if (contract.alienLevel < 0)
            _alienLevel.text = "????";
        else
            _alienLevel.text = $"{contract.alienLevel}";
        _pirateLevel.text = $"{contract.pirateLevel}";
        
        _routeLength.text = $"{contract.routeLength.ToString("N0")}";
        _routeTime.text = $"{contract.routeTime.ToString("N0")}";
        
        _buttons[(int) ContractButtonType.Accept].SetActive(!accepted);
        _buttons[(int) ContractButtonType.Decline].SetActive(!accepted);
        _buttons[(int) ContractButtonType.Abort].SetActive(accepted);
    }

    public bool RequirementsMet()
    {
        if (Mathf.CeilToInt(GameController.Instance.ship.GetFuelPercent() * 100) < _contract.averageFuelPercentNeeded)
            return false;
        else if (GameController.Instance.ship.GetCrewCount() < _contract.crewMembersNeeded)
            return false;

        return true;
    }
}
