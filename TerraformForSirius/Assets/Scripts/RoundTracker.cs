using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
public class RoundTracker:MonoBehaviour
{
    [SerializeField] private Resources currentResources;
    [SerializeField] private float maxKadir;
    [ShowInInspector] private int _availablePopulation;
    [ShowInInspector] private float _availableKadir;
    private int _roundCount;

    private void OnEnable()
    {
        EventManager.ConsumeResource += ConsumeResources;
        EventManager.AvailablePopulationChanged += AdjustAvailablePopulation;
        EventManager.AvailableKadirChanged += AdjustAvailableKadir;
        EventManager.NextTurn += OnTurnPassed;
        _availableKadir = currentResources.Kadir;
        _availablePopulation = currentResources.Population;
    }

    private void AdjustAvailableKadir(float x)
    {
        _availableKadir += x;
    }

    private void AdjustAvailablePopulation(int x)
    {
        _availablePopulation += x;
    }

    private void OnDisable()
    {
        EventManager.ConsumeResource -= ConsumeResources;
        EventManager.AvailablePopulationChanged -= AdjustAvailablePopulation;
        EventManager.AvailableKadirChanged -= AdjustAvailableKadir;
        EventManager.NextTurn -= OnTurnPassed;
    }

    private void OnTurnPassed()
    {
        EventManager.OnGenerateResources(currentResources);
        _availableKadir = maxKadir;
        _availablePopulation = currentResources.Population;
        EventManager.OnAdjustTileControlUI(currentResources);
    }

    private void ConsumeResources(Resources resources)
    {
        currentResources.Consume(resources);
    }


    public bool AreConditionsSufficient(Resources resources)
    {
        return currentResources.IsGreaterThan(resources);
    }

    public int GetAvailablePopulation()
    {
        return _availablePopulation;
    }

    public float GetAvailableKadir()
    {
        return _availableKadir;
    }
}
