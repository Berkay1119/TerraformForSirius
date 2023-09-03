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
    [SerializeField] private Slider kadirSlider;
    [SerializeField] private Slider availablePopulationSlider;
    [SerializeField] private Slider planetHealthSlider;
    [SerializeField] private RoundTracker _roundTracker;


    private void OnEnable()
    {
        EventManager.ResourceUpdated+=RefreshUI;
    }

    private void OnDisable()
    {
        EventManager.ResourceUpdated-=RefreshUI;
    }

    private void RefreshUI()
    {
        foodText.text = _roundTracker.GetCurrentResources().Food.ToString();
    }
}
