using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class HexagonalCard : MonoBehaviour
{
    [ShowInInspector] private string _name;
    [ShowInInspector] private Sprite _sprite;
    [ShowInInspector] private Resources _constructionRequirement;
    [ShowInInspector] private Resources _operationalRequirement;
    [ShowInInspector] private Resources _outcomeResources;
    [ShowInInspector] private int _barrier;
    private HexagonalGrid _assignedGrid;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TileControlUI populationAdjustmentControlUI;
    [SerializeField] private TileControlUI kadirAdjustmentControlUI;
    [SerializeField] private InGameCanvasScript canvas;
    private void OnEnable()
    {
        EventManager.GenerateResources += GenerateOutCome;
    }

    private void OnDisable()
    {
        EventManager.GenerateResources -= GenerateOutCome;
    }

    private void OnMouseDown()
    {
        if (IsCardPlaced())
        {
            if (canvas.IsPointerOverGameObject())
            {
                return;
            }
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
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
        spriteRenderer.sprite = _sprite;
    }

    public void Play(HexagonalGrid hexagonalGrid)
    {
        transform.position = hexagonalGrid.transform.position;
        EventManager.OnConsumeResource(_constructionRequirement);
        EventManager.OnCardPlayed(this);
        _assignedGrid = hexagonalGrid;

    }

    public void GenerateOutCome(Resources currentResources)
    {
        if (_assignedGrid==null)
        {
            return;
        }
        if (populationAdjustmentControlUI.GetPreviousSliderValue()>=_operationalRequirement.Population)
        {
            if (kadirAdjustmentControlUI.GetPreviousSliderValue()>=_operationalRequirement.Kadir)
            {
                if (_operationalRequirement.IsGreaterThan(currentResources))
                {
                    return;
                }
                currentResources.Generate(_outcomeResources);
            }
        }
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
