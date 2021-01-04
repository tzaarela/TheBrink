using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Crew;
using Assets.Scripts.InterfacePanels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EmployeeContent : UITrigger
{
    [SerializeField] private CrewData _crewData; 
    public CrewData CrewData => _crewData;

    [Header("UI Elements")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _portrait;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _age;
    [SerializeField] private TMP_Text _gender;
    [SerializeField] private TMP_Text _quirk;
    [SerializeField] private TMP_Text _length;
    [SerializeField] private TMP_Text _profession;
    [SerializeField] private TMP_Text _bloodType;

    private string[] _bloodTypes = new string[8] {"A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-"};
    
    private bool _mouseOver;
    private const float ClickVolume = 0.5f;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // UpdateUI();
    }
    
    public void UpdateUI()
    {
        UpdateUI(_crewData);
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

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetMouseButtonUp(0))
            return;
        
        // base.OnPointerClick(eventData);
        SpaceportBarrackController.instance.ShowEmployeeContract(_crewData, this);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        _mouseOver = true;
        _background.color = new Color(0.8f, 0.9f, 0.9f, 1f);
    }
    
    public override void OnPointerExit(PointerEventData eventData)
    {
        _mouseOver = false;
        _background.color = new Color32(205, 205, 205, 255);
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        // base.OnPointerUp(eventData);
        if (!_mouseOver)
            return;

        if (Input.GetMouseButtonUp(0))
            AudioController.instance.PlaySFX(SFXClipType.ButtonClickUp, ClickVolume);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // base.OnPointerDown(eventData);
        if (!_mouseOver || !Input.GetMouseButton(0))
            return;
        
        AudioController.instance.PlaySFX(SFXClipType.ButtonClickDown, ClickVolume);
    }
}
