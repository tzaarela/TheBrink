using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.InterfacePanels;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : UITrigger
{
    private Image _image;
    private TMP_Text _text;
    [SerializeField] private Sprite[] _sprites = new Sprite[5];
    [SerializeField] private Color[] _colors = new Color[5];
    private bool _mouseOver;

    private void Awake()
    {
        GetAllComponents();
    }

    private void Start()
    {
        // Debug.Log($"_sprites.Length: {_sprites.Length}");
        // Debug.Log($"_colors.Length: {_colors.Length}");
        SetButtonState(ButtonState.Normal);
    }

    private void GetAllComponents()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void SetButtonState(ButtonState buttonState)
    {
        _image.sprite = _sprites[(int) buttonState];
        _text.color = _colors[(int) buttonState];
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        SetButtonState(ButtonState.Highlight);
        _mouseOver = true;
    }
    
    public override void OnPointerExit(PointerEventData eventData)
    {
        SetButtonState(ButtonState.Normal);
        _mouseOver = false;
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!_mouseOver)
            return;
        
        SetButtonState(ButtonState.Highlight);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!_mouseOver)
            return;
        
        SetButtonState(ButtonState.Pressed);
    }
}
