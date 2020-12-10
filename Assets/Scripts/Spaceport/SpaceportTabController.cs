using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceportTabController : MonoBehaviour
{
    public static SpaceportTabController instance;
    [SerializeField] private SpaceportTabObject[] _tabs;
    
    private Image[] _images;
    [SerializeField] private Sprite[] _sprites;
    private TMP_Text[] _texts;
    [SerializeField] private Color[] _colors;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        GetAllComponents();
    }

    private void Start()
    {
        SelectTab(_tabs[(int)SpaceportTabType.Contracts]);
    }

    private void GetAllComponents()
    {
        _tabs = new SpaceportTabObject[transform.childCount];
        for (int i = 0; i < _tabs.Length; i++)
        {
            _tabs[i] = transform.GetChild(i).GetComponent<SpaceportTabObject>();
        }
        
        _images = new Image[_tabs.Length];
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i] = transform.GetChild(i).GetComponent<Image>();
        }
        
        _texts = new TMP_Text[_images.Length];
        for (int i = 0; i < _texts.Length; i++)
        {
            _texts[i] = _images[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void ResetAllPanels()
    {
        foreach (Image image in _images)
        {
            image.sprite = _sprites[(int) TabState.Inactive];
        }

        foreach (TMP_Text text in _texts)
        {
            text.color = _colors[(int) TabState.Inactive];
        }

        foreach (SpaceportTabObject tab in _tabs)
        {
            tab.tabState = TabState.Inactive;
        }
    }

    public void SelectTab(SpaceportTabObject tab)
    {
        ResetAllPanels();

        _tabs[(int) tab.tabType].tabState = TabState.Active;
        _images[(int)tab.tabType].sprite = _sprites[(int)TabState.Active * 3 + (int)ButtonState.Highlight];
        _texts[(int)tab.tabType].color = _colors[(int)TabState.Active * 3];
    }

    public void SetButtonState(SpaceportTabObject tab, ButtonState buttonState)
    {
        // Debug.Log($"tabType: {tab.tabType}({(int)tab.tabType}) - tab.tabState: {tab.tabState}({((tab.tabState == TabState.Active) ? 3 : 0)}) * buttonState: {buttonState}({(int)buttonState}) = {((tab.tabState == TabState.Active) ? 3 : 0) + (int)buttonState}");
        _images[(int)tab.tabType].sprite = _sprites[((tab.tabState == TabState.Active) ? 3 : 0) + (int)buttonState];
        _texts[(int) tab.tabType].color = _colors[(int)buttonState];
    }
}
