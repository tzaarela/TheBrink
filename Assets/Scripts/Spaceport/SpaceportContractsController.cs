using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceportContractsController : SpaceportPanelController
{
    public static SpaceportContractsController instance;
    
    [SerializeField] private MissionContract[] _missionContracts = new MissionContract[3];
    [SerializeField] private TMP_Text[] _contractsShortDescription = new TMP_Text[3];
    [SerializeField] private TMP_Text[] _contractsActive = new TMP_Text[3];

    [SerializeField] private ContractContent _contractContent;
    private int _activeContractIndex = -1;
    private bool _accepted;

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
        
        Init();
    }

    protected override void GetAllComponents()
    {
        base.GetAllComponents();
    }

    private void Init()
    {
        _activeContractIndex = -1;
        SetAllPanels(false);
        SetPanel((int)ContractsPanelType.List, true);

        UpdateContractsDescriptionUI();
    }

    private void UpdateContractsDescriptionUI()
    {
        for (int i = 0; i < _contractsShortDescription.Length; i++)
        {
            if (_missionContracts[i] == null)
            {
                _contractsShortDescription[i].transform.parent.gameObject.SetActive(false);
                continue;
            }
            
            _contractsActive[i].gameObject.SetActive(_activeContractIndex == i);

            MissionContract missionContract = _missionContracts[i];
            
            _contractsShortDescription[i].transform.parent.gameObject.SetActive(true);
            _contractsShortDescription[i].text = $"{missionContract.contractName}\n\n{missionContract.shortDescription}";
        }
    }

    public void ViewContract(int index)
    {
        SetAllPanels(false);

        _activeContractIndex = index;
        _contractContent.SetupContract(_missionContracts[index], _accepted);
        
        SetPanel((int) ContractsPanelType.Info, true);
        SetPanel((int) ContractsPanelType.GalaxyMap, true);
    }

    public void Show()
    {
        SetAllPanels(false);
        
        SetPanel((int) ContractsPanelType.List, true);
    }

    public void AcceptContract()
    {
        if (_contractContent.RequirementsMet())
        {
            GameController.Instance.ship.missionContract = _missionContracts[_activeContractIndex];
            _accepted = true;
            UpdateContractsDescriptionUI();
            Show();
        }
        else
        {
            
        }
    }
    
    public void DeclineContract()
    {
        Show();
    }

    public void AbortContract()
    {
        GameController.Instance.ship.missionContract = null;
        _activeContractIndex = -1;
        _accepted = false;
        UpdateContractsDescriptionUI();
        Show();
    }
}

enum ContractsPanelType : int
{
    List,
    Info,
    GalaxyMap
}
