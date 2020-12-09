using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.InterfacePanels;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceportTabObject : UITrigger
{
    [SerializeField] private SpaceportTabType _tabType;

    public override void OnPointerClick(PointerEventData eventData)
    {
        // base.OnPointerClick(eventData);
        // Debug.Log($"{name} was clicked on!");
        SpaceportTabController.instance.SelectTab(_tabType);
    }
}
