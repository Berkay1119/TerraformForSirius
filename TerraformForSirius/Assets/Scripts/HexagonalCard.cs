using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HexagonalCard : MonoBehaviour
{
    [ShowInInspector] private string name;
    [ShowInInspector] private Sprite sprite;
    [ShowInInspector] private Resources ConstructionRequirement;
    [ShowInInspector] private Resources OperationalRequirement;
    [ShowInInspector] private Resources OutcomeResources;
    [ShowInInspector] private int barrier;

    private void OnMouseDown()
    {
        EventManager.OnCardSelected(this);
    }

    public void AssignData(CardDataSO cardDataSo)
    {
        this.name = cardDataSo.name;
        this.sprite = cardDataSo.sprite;
        this.ConstructionRequirement = cardDataSo.ConstructionRequirement;
        this.OperationalRequirement = cardDataSo.OperationalRequirement;
        this.OutcomeResources = cardDataSo.OutcomeResources;
        this.barrier = cardDataSo.barrier;
    }
}
