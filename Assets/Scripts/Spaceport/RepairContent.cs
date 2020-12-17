using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.InterfacePanels;
using Assets.Scripts.Rooms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class RepairContent : UITrigger
{
    [SerializeField] private RoomData _roomData;

    [SerializeField] private GameObject _selectionObject;

    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private TMP_Text _roomCost;

    [SerializeField] private Image _fillBar;
    [SerializeField] private GameObject _warningIcon;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _warningColor;

    private float _warningLimit = 0.4f;

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => _isSelected = value;
    }

    private const int MaxRoomHP = 100; 

    private void Awake()
    {
        SetSelectedObject(_isSelected = false);
    }

    public void Init()
    {
        if (_roomData == null)
            return;
        
        _roomName.text = Enum.GetName(typeof(RoomType), _roomData.roomType);

        UpdateUI();
    }

    public void Init(string roomName, int cost)
    {
        _roomName.text = roomName;
        _roomCost.text = "$" + cost.ToString("N0");;
    }

    public void Reset()
    {
        _roomData.health = MaxRoomHP;
    }

    public void ToggleSelectedObject()
    {
        SetSelectedObject(!_isSelected);
        SpaceportWorkshopController.instance.UpdateTotalRepairCostUI();
    }

    public void SetSelectedObject(bool active)
    {
        _selectionObject.SetActive(_isSelected = active);
    }

    private float GetHealthPercent()
    {
        return _roomData.health / MaxRoomHP;
    }

    public void SetFullHealth()
    {
        _roomData.SetFullHealth();
        UpdateUI();
    }

    private void UpdateUI()
    {
        _fillBar.fillAmount = GetHealthPercent();
        _warningIcon.SetActive(_fillBar.fillAmount <= _warningLimit);
        _fillBar.color = (_warningIcon.activeSelf) ? _warningColor : _normalColor;
        _roomCost.text = "$" + GetCost().ToString("N0");
    }

    public int GetCost()
    {
        return (MaxRoomHP - Mathf.CeilToInt(_roomData.health)) * 100;
    }

    public void UpdateCostText(int cost)
    {
        _roomCost.text = "$" + cost.ToString("N0");;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }
}
