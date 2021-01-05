using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.InterfacePanels;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class VolumeSlider : UITrigger
{
    private Image _image;
    private Camera _camera;
    private bool _isDraging;

    [SerializeField] private GameObject _checkBox;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private VolumeType _volumeType;

    private const float ClickVolume = 0.5f;

    private void Awake()
    {
        GetAllComponents();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (!_isDraging)
            return;
        
        OnChangedValue();
    }

    private void GetAllComponents()
    {
        _camera = Camera.main;
        _image = GetComponent<Image>();
    }

    private void Init()
    {
        float volumePercent = AudioController.instance.GetGlobalVolume(_volumeType);
        _image.fillAmount = volumePercent;
        _text.text = $"{Mathf.RoundToInt(volumePercent * 100)}";
        UpdateUI();
    }

    public void OnChangedValue()
    {
        Vector2 _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        float percent = Mathf.Clamp01((_mousePos.x - transform.position.x) / _image.rectTransform.rect.width);

        _image.fillAmount = percent;
        
        AudioController.instance.SetGlobalVolume(_volumeType, percent);
        _text.text = $"{Mathf.RoundToInt(percent * 100)}";
    }

    public void ToggleMute()
    {
        AudioController.instance.ToggleMute(_volumeType);
        UpdateUI();
    }

    private void UpdateUI()
    {
        _checkBox.SetActive(!AudioController.instance.IsVolumeMuted(_volumeType));
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(0))
            return;

        _isDraging = true;
        AudioController.instance.PlaySFX(SFXClipType.ButtonClickDown, ClickVolume);
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!Input.GetMouseButtonUp(0))
            return;

        _isDraging = false;
    }
}
