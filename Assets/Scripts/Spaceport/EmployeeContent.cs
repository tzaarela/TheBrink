using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Crew;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeContent : MonoBehaviour
{
    [SerializeField] private CrewData _crewData;
    
    [Header("UI Elements")]
    [SerializeField] private Image _portrait;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _age;
    [SerializeField] private TMP_Text _gender;
    [SerializeField] private TMP_Text _quirk;
    [SerializeField] private TMP_Text _length;
    [SerializeField] private TMP_Text _profession;
    [SerializeField] private TMP_Text _bloodType;

    private string[] _bloodTypes = new string[8] {"A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-"};

    private void Awake()
    {
    }

    private void Init()
    {
    }

    public void UpdateUI(CrewData crewData)
    {
        _crewData = crewData;

        _portrait.sprite = _crewData.sprite;
        _name.text = _crewData.crewName;
        _age.text = $"{_crewData.age}";
        _gender.text = $"{_crewData.gender}";
        _quirk.text = _crewData.quirk;
        _length.text = $"{_crewData.lengthCM}cm";
        _profession.text = $"{_crewData.profession}";
        _bloodType.text = _bloodTypes[(int) _crewData.bloodType];
    }
}
