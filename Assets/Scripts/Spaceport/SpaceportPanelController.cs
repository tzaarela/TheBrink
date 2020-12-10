using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceportPanelController : MonoBehaviour
{
    protected GameObject _rootPanel;
    protected SpaceportTabType _tabType; 

    [SerializeField] protected GameObject[] _panels;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void GetAllComponents()
    {
        _rootPanel = transform.GetChild((int)_tabType).gameObject;
    }
    
    protected void SetAllPanels(bool active)
    {
        foreach (GameObject panel in _panels)
        {
            panel.SetActive(active);
        }
    }

    public void SetRootPanel(bool active)
    {
        _rootPanel.SetActive(active);
    }

    protected void SetPanel(int panelIndex, bool active)
    {
        _panels[panelIndex].SetActive(active);
    }
}
