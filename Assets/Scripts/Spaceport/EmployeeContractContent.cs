using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Crew;
using UnityEngine;
using TMPro;

public class EmployeeContractContent : MonoBehaviour
{
    [SerializeField] private CrewData _crewData;
    public CrewData CrewData
    {
        get => _crewData;
        set => _crewData = value;
    }

    [Header("Contract Info")]
    [SerializeField] private TMP_Text _companyName;
    [SerializeField] private TMP_Text _employeeInfo;
    [SerializeField] private TMP_Text _workHours;
    [SerializeField] private TMP_Text _profession;
    [SerializeField] private TMP_Text _employeeHistory;
    [SerializeField] private TMP_Text _salary;
    [SerializeField] private TMP_Text _summary;

    [Header("Buttons")]
    [SerializeField] private GameObject _signButton;
    [SerializeField] private GameObject _fireButton;

    [Header("Extra")]
    [SerializeField] private GameObject _stamp;

    public void UpdateUI(CrewData crewData)
    {
        _crewData = crewData;

        _companyName.text = _crewData.companyName;
        _employeeInfo.text = $"Name: {_crewData.crewName}\n" +
                             $"Age: {_crewData.age} Y/O\n" +
                             $"Favorite Food: {_crewData.favoriteFood}";
        _workHours.text = _crewData.workHours;
        _profession.text = $"{_crewData.profession}";
        _employeeHistory.text = _crewData.employeeHistory;
        _salary.text = $"${_crewData.salary.ToString("N0")}";
        _summary.text = _crewData.summary;
        
        _signButton.SetActive(!_crewData.isHired);
        _fireButton.SetActive(_crewData.isHired);
        _stamp.SetActive(_crewData.isHired);
    }
}
