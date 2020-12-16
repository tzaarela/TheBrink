using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelContent : MonoBehaviour
{
    [SerializeField] private Image _currentFuelImage;
    [SerializeField] private Image _changedFuelImage;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _negativeColor;
    [SerializeField] private Color _positiveColor;

    private float _currentFuel;

    public void UpdateUI(float amount, bool applyChanges)
    {
        if (applyChanges)
        {
            _currentFuel = amount;
            
            _currentFuelImage.fillAmount = _currentFuel;
            _changedFuelImage.fillAmount = _currentFuel;
        }
        else
        {
            if (amount < _currentFuel)
            {
                _currentFuelImage.fillAmount = amount;
                
                _changedFuelImage.fillAmount = _currentFuel;
                _changedFuelImage.color = _negativeColor;
            }
            // else if (amount > _currentFuel)
            else
            {
                _currentFuelImage.fillAmount = _currentFuel;
                
                _changedFuelImage.fillAmount = amount;
                _changedFuelImage.color = _positiveColor;
            }
        }
    }
}