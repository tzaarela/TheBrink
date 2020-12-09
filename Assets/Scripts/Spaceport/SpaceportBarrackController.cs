using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceportBarrackController : SpaceportPanelController
{
    public static SpaceportBarrackController instance;

    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        base.Awake();
        
        _tabType = SpaceportTabType.Barrack;

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
