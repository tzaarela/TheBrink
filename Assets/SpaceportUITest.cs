using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceportUITest : MonoBehaviour
{
    [SerializeField] private GameObject _tempCanvas;
    [SerializeField] private GameObject _spaceportCanvas;
    [SerializeField] private GameObject _contractsPanel;
    [SerializeField] private GameObject _contractsInfoPanel;
    [SerializeField] private GameObject _barrackPanel;
    
    [SerializeField] private GameObject _contractViewButtons;
    [SerializeField] private GameObject _contractAcceptButtons;

    private void HideAllPanels()
    {
        _tempCanvas.SetActive(false);
        _spaceportCanvas.SetActive(false);
        
        _contractViewButtons.SetActive(false);
        _contractAcceptButtons.SetActive(false);
        
        _contractsPanel.SetActive(false);
        _contractsInfoPanel.SetActive(false);
        _barrackPanel.SetActive(false);
    }

    public void ContractPress1()
    {
        Debug.Log($"ContractPress1");
        HideAllPanels();
        
        _spaceportCanvas.SetActive(true);
        
        _contractViewButtons.SetActive(true);
        
        _contractsPanel.SetActive(true);
    }
    
    public void ViewContract()
    {
        Debug.Log($"ViewContract");
        HideAllPanels();
        
        _spaceportCanvas.SetActive(true);
        
        _contractAcceptButtons.SetActive(true);
        
        _contractsPanel.SetActive(true);
        _contractsInfoPanel.SetActive(true);
    }
    
    public void BarrackPress()
    {
        Debug.Log($"BarrackPress");
        HideAllPanels();
        
        _spaceportCanvas.SetActive(true);

        _barrackPanel.SetActive(true);
    }
    
    public void WorkshopPress()
    {
        Debug.Log($"WorkshopPress");
        HideAllPanels();
        
        _tempCanvas.SetActive(true);
    }
    
    public void DepartPress()
    {
        Debug.Log($"DepartPress");
        SceneManager.LoadScene("JEmdi");
    }
}
