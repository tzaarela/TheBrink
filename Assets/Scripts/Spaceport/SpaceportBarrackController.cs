using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Crew;
using UnityEngine;

public class SpaceportBarrackController : SpaceportPanelController
{
    public static SpaceportBarrackController instance;

    [SerializeField] private EmployeeContractContent _employeeContract;
    [SerializeField] private Transform _crewTransform;
    [SerializeField] private Transform _unemployedTransform;

    [SerializeField] private CrewData[] _crewMembersData = new CrewData[3];
    [SerializeField] private List<EmployeeContent> _crewMembersContent;
    [SerializeField] private List<EmployeeContent> _employesContent;

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
        
        Init();
    }

    protected override void GetAllComponents()
    {
        base.GetAllComponents();
    }

    private void Init()
    {
        // _employeeContract.gameObject.SetActive(false);

        InitEmployeeContent(ref _crewMembersContent, ref _crewTransform);
        InitEmployeeContent(ref _employesContent, ref _unemployedTransform);
        
        _employeeContract.UpdateUI(_crewMembersData[0]);
    }

    private void InitEmployeeContent(ref List<EmployeeContent> contentList, ref Transform contentTransform)
    {
        contentList = new List<EmployeeContent>();
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            contentList.Add(contentTransform.GetChild(i).GetComponent<EmployeeContent>());
            
            CrewData crewData = _crewMembersData[i] != null
                ? _crewMembersData[i]
                : (CrewData) ScriptableObject.CreateInstance(typeof(CrewData));
            
            contentList[i].UpdateUI(crewData);
        }
    }

    public void ShowEmployeeContract()
    {
        _employeeContract.gameObject.SetActive(true);
    }
}
