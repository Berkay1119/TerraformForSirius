﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
public class RoundTracker:MonoBehaviour
{
    [SerializeField] private Resources currentResources;
    [SerializeField] private float maxKadir;
    [ShowInInspector] private int _availablePopulation;
    [ShowInInspector] private float _availableKadir;
    [ShowInInspector] private int _barrier;
    [SerializeField] private List<AlienInfo> aliens;
    private int _roundCount=1;

    private int maxPlanetHealth = 100; // Default value set to 100.


    private void OnEnable()
    {
        EventManager.ConsumeResource += ConsumeResources;
        EventManager.AvailablePopulationChanged += AdjustAvailablePopulation;
        EventManager.AvailableKadirChanged += AdjustAvailableKadir;
        EventManager.NextTurn += OnTurnPassed;
        EventManager.BarrierIncreased +=OnBarrierIncreased;
        _availableKadir = currentResources.Kadir;
        _availablePopulation = currentResources.Population;
        EventManager.BarrierHasBeenDamaged += OnBarrierDamaged;
    }

    private void Start()
    {
        EventManager.OnAdjustTileControlUI(currentResources);
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
        EventManager.BarrierIncreased -=OnBarrierIncreased;
        EventManager.BarrierHasBeenDamaged -= OnBarrierDamaged;
    }

    private void OnBarrierDamaged(int x)
    {
        _barrier -= x;
        if (_barrier<0)
        {
           EventManager.OnGameLost(); 
        }
    }

    private void OnTurnPassed()
    {
        EventManager.OnGenerateResources(currentResources);
        _availableKadir = maxKadir;
        _availablePopulation = currentResources.Population;
        foreach (var alienInfo in aliens)
        {
            if (alienInfo.round==_roundCount)
            {
                EventManager.AliensAreComing(alienInfo);
            }
        }
        _roundCount++;
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

    private void OnBarrierIncreased(int x)
    {
        _barrier += x;
    }

     public int GetMaxPlanetHealth()
    {
        return maxPlanetHealth;
    }

    public Resources GetCurrentResources()
    {
        return currentResources;
    }

    public float GetMaxKadir()
    {
        return maxKadir;
    }

    public int GetCurrentBarrier()
    {
        return _barrier;
    }
}

[Serializable]
public class AlienInfo
{
    public int round;
    public int damage;
}
