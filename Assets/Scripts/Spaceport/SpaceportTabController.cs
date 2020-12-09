using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceportTabController : MonoBehaviour
{
    public static SpaceportTabController instance;
    
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
        SelectTab(SpaceportTabType.Contracts);
    }

    private void GetAllComponents()
    {
        _images = new Image[transform.childCount];
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
    }

    public void SelectTab(SpaceportTabType tabType)
    {
        ResetAllPanels();
        
        _images[(int)tabType].sprite = _sprites[(int)TabState.Active];
        _texts[(int)tabType].color = _colors[(int)TabState.Active];
    }
}

enum TabState : int
{
    Inactive,
    Active
}
