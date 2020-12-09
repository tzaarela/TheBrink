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
    }

    protected override void GetAllComponents()
    {
        base.GetAllComponents();
    }
}
