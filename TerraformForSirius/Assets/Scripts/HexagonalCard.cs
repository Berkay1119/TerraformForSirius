using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

public class HexagonalCard : MonoBehaviour
{
    [ShowInInspector] private string _name;
    [ShowInInspector] private Sprite _sprite;
    [ShowInInspector] private Resources _constructionRequirement;
    [ShowInInspector] private Resources _operationalRequirement;
    [ShowInInspector] private Resources _outcomeResources;
    [ShowInInspector] private int _barrier;
    private HexagonalGrid _assignedGrid;

    private void OnMouseDown()
    {
        if (IsCardPlaced())
        {
            return;    
        }
        EventManager.OnCardSelected(this);
    }

    public void AssignData(CardDataSO cardDataSo)
    {
        this._name = cardDataSo.name;
        this._sprite = cardDataSo.sprite;
        this._constructionRequirement = cardDataSo.ConstructionRequirement;
        this._operationalRequirement = cardDataSo.OperationalRequirement;
        this._outcomeResources = cardDataSo.OutcomeResources;
        this._barrier = cardDataSo.barrier;
    }

    public void Play(HexagonalGrid hexagonalGrid)
    {
        transform.position = hexagonalGrid.transform.position;
        EventManager.OnConsumeResource(_constructionRequirement);
        EventManager.OnCardPlayed(this);
        _assignedGrid = hexagonalGrid;

    }

    private bool IsCardPlaced()
    {
        return _assignedGrid != null;
    }
    
    public Resources GetResources()
    {
        return _constructionRequirement;
    }
}
