using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceportContractsController : SpaceportPanelController
{
    public static SpaceportContractsController instance;

    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        base.Awake();

        _tabType = SpaceportTabType.Contracts;

        GetAllComponents();
    }

    protected override void Start()
    {
        base.Start();
        
        SetAllPanels(false);
        SetPanel((int)ContractsPanelType.List, true);
    }

    protected override void GetAllComponents()
    {
        base.GetAllComponents();
    }
}

enum ContractsPanelType : int
{
    List,
    Info,
    GalaxyMap
}
