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

    [SerializeField] private Image _roomHighlight;
    [SerializeField] private Image _fillBar;
    [SerializeField] private GameObject _warningIcon;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _warningColor;

    [SerializeField] private Color[] _highlightColors;

    private const float WarningLimit = 0.7f;
    private const float DangerLimit = 0.3f;

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
        float healthPercent = GetHealthPercent();
        _fillBar.fillAmount = healthPercent;
        _warningIcon.SetActive(_fillBar.fillAmount <= DangerLimit);
        // _fillBar.color = (_warningIcon.activeSelf) ? _warningColor : _normalColor;
        _roomCost.text = "$" + GetCost().ToString("N0");

        if (healthPercent <= DangerLimit)
        {
            _roomHighlight.color = _highlightColors[(int) ShipHighlightColorType.Danger];
            _fillBar.color = _highlightColors[(int) ShipHighlightColorType.Danger];
        }
        else if (healthPercent <= WarningLimit)
        {
            _roomHighlight.color = _highlightColors[(int) ShipHighlightColorType.Warning];
            _fillBar.color = _highlightColors[(int) ShipHighlightColorType.Warning];
        }
        else if (healthPercent < 1f)
        {
            _roomHighlight.color = _highlightColors[(int) ShipHighlightColorType.Fine];
            _fillBar.color = _highlightColors[(int) ShipHighlightColorType.Fine];
            // _fillBar.color = _highlightColors[(int) ShipHighlightColorType.Fine];
        }
        else
        {
            _roomHighlight.color = _normalColor;
            _roomHighlight.color = new Color(_roomHighlight.color.r, _roomHighlight.color.g, _roomHighlight.color.b, 0.4f);
            _fillBar.color = _normalColor;
        }

        _fillBar.color = new Color(_fillBar.color.r, _fillBar.color.g, _fillBar.color.b, 1f);
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
