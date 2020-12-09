using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceportWorkshopController : SpaceportPanelController
{
    public static SpaceportWorkshopController instance;
    
    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        base.Awake();
        
        _tabType = SpaceportTabType.Workshop;

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
