using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using Assets.Scripts.Crew;
using UnityEngine;

public class SpaceportBarrackController : SpaceportPanelController
{
    public static SpaceportBarrackController instance;

    [SerializeField] private EmployeeContractContent _employeeContract;
    [SerializeField] private Transform _crewTransform;
    [SerializeField] private Transform _unemployedTransform;

    [SerializeField] private Crew _crew;
    [SerializeField] private List<EmployeeContent> _employeesContent;
    [SerializeField] private EmployeeContent _currentEmployeeContent;

    private const int MaxCrewMembers = 3;

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
        _crew = GameController.Instance.crew;
        InitEmployeeContent(ref _employeesContent, ref _unemployedTransform);
        
        _employeeContract.gameObject.SetActive(false);
    }

    private void InitEmployeeContent(ref List<EmployeeContent> contentList, ref Transform contentTransform)
    {
        contentList = new List<EmployeeContent>();
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            contentList.Add(contentTransform.GetChild(i).GetComponent<EmployeeContent>());

            CrewData crewData = contentList[i].CrewData == null
                ? (CrewData) ScriptableObject.CreateInstance(typeof(CrewData))
                : contentList[i].CrewData;

            contentList[i].UpdateUI(crewData);
        }

        foreach (EmployeeContent content in contentList)
        {
            CrewData crewData = content.CrewData;
            content.transform.SetParent(crewData.isHired ? _crewTransform : _unemployedTransform);

            if (!_crew.crewDataList.Contains(crewData) && crewData.isHired)
                _crew.crewDataList.Add(crewData);
            else if (_crew.crewDataList.Contains(crewData) && !crewData.isHired)
                _crew.crewDataList.Remove(crewData);
        }
    }

    public void ShowEmployeeContract(CrewData crewData, EmployeeContent employeeContent)
    {
        _employeeContract.UpdateUI(crewData);
        _currentEmployeeContent = employeeContent;
        SetEmployeeContract(true);
    }
    
    private void SetEmployeeContract(bool active)
    {
        _employeeContract.gameObject.SetActive(active);
    }

    public void HireEmployee()
    {
        if (_crew.crewDataList.Count >= MaxCrewMembers)
            return;
        
        SetEmployeeStatus(true);
    }
    
    public void FireEmployee()
    {
        SetEmployeeStatus(false);
    }

    private void SetEmployeeStatus(bool hire)
    {
        CrewData crewData = _employeeContract.CrewData;
        crewData.isHired = hire;

        if (hire)
            _crew.crewDataList.Add(crewData);
        else
            _crew.crewDataList.Remove(crewData);
        
        _currentEmployeeContent.transform.SetParent(hire ? _crewTransform : _unemployedTransform);

        SetEmployeeContract(false);
    }
}
