using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.InterfacePanels;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceportTabObject : UITrigger
{
    public SpaceportTabType tabType;
    [HideInInspector] public TabState tabState;
    private bool _mouseOver;
    private const float ClickVolume = 0.5f;

    private void Awake()
    {
        if (tabType == SpaceportTabType.Contracts)
            tabState = TabState.Active;
        else
            tabState = TabState.Inactive;
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetMouseButtonUp(0))
            return;
        
        SpaceportTabController.instance.SetButtonState(this, ButtonState.Pressed);
        SpaceportTabController.instance.SelectTab(this);
        SpaceportUIController.instance.ShowPanel(tabType);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        SpaceportTabController.instance.SetButtonState(this, ButtonState.Highlight);
        _mouseOver = true;
    }
    
    public override void OnPointerExit(PointerEventData eventData)
    {
        SpaceportTabController.instance.SetButtonState(this, ButtonState.Normal);
        _mouseOver = false;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!_mouseOver)
            return;
        
        SpaceportTabController.instance.SetButtonState(this, ButtonState.Highlight);
        
        if (Input.GetMouseButtonUp(0))
            AudioController.instance.PlaySFX(SFXClipType.ButtonClickUp, ClickVolume);
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!_mouseOver || !Input.GetMouseButton(0))
            return;
        
        SpaceportTabController.instance.SetButtonState(this, ButtonState.Pressed);
        AudioController.instance.PlaySFX(SFXClipType.ButtonClickDown, ClickVolume);
    }
}
