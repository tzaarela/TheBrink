using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemWindowController : MonoBehaviour
{
    public static SystemWindowController Instance;

    List<SystemContent> systemContents;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        systemContents = gameObject.GetComponentsInChildren<SystemContent>(true).ToList();
    }

    public void InitSystemContent()
    {
        systemContents.ForEach(x => x.Init());
    }

    public void UpdateUISystemContent()
    {
        systemContents.ForEach(x => x.UpdateUI());
    }
}
