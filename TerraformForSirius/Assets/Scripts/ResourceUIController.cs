using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResourceUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI mineText;
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI totalPopulationText;
    [SerializeField] private TextMeshProUGUI barrierText;
    [SerializeField] private Slider kadirSlider;
    [SerializeField] private Slider availablePopulationSlider;
    [SerializeField] private Slider planetHealthSlider;
    [SerializeField] private RoundTracker _roundTracker;


    
    
    private void OnEnable()
    {
        EventManager.ResourceUpdated+=RefreshUI;
        RefreshUI();
    }

    private void OnDisable()
    {
        EventManager.ResourceUpdated-=RefreshUI;
    }

    private void RefreshUI()
    {
        foodText.text = _roundTracker.GetCurrentResources().Food.ToString();
        mineText.text = _roundTracker.GetCurrentResources().Mine.ToString();
        waterText.text = _roundTracker.GetCurrentResources().Water.ToString();
        barrierText.text = _roundTracker.GetCurrentBarrier().ToString();
        totalPopulationText.text = _roundTracker.GetCurrentResources().Population.ToString();
        kadirSlider.value = (float)_roundTracker.GetAvailableKadir() / _roundTracker.GetMaxKadir();
        availablePopulationSlider.value = (float)_roundTracker.GetAvailablePopulation() /
                                          _roundTracker.GetCurrentResources().Population;
        planetHealthSlider.value =
            (float)_roundTracker.GetCurrentResources().PlanetHealth / _roundTracker.GetMaxPlanetHealth();
    }
    
    
}
