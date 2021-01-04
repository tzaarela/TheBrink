using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.InterfacePanels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeSlider : UITrigger
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _mousePos;
    [SerializeField] private Vector2 _mouseWorldPos;
    [SerializeField] private Vector2 _pos;
    [SerializeField] private float _diff;
    

    [SerializeField] private Image _image;

    private void Awake()
    {
        _camera = Camera.main;
        _image = GetComponent<Image>();

        _pos = transform.position;
    }

    public void OnChangedValue()
    {
        _mousePos = Input.mousePosition;
        _mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        _pos = transform.position;
        
        _diff = _mouseWorldPos.x - _pos.x;
        
        float percent = _diff / _image.rectTransform.rect.width;

        _image.fillAmount = percent;

    }

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
        OnChangedValue();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(0))
            return;
        
        OnChangedValue();
    }
}
