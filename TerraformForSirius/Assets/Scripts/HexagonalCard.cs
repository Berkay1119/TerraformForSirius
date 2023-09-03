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
    [ShowInInspector] private List<Sprite> _sprites;
    [ShowInInspector] private Resources _constructionRequirement;
    [ShowInInspector] private Resources _operationalRequirement;
    [ShowInInspector] private Resources _outcomeResources;
    [ShowInInspector] private int _startingBarrier;
    [ShowInInspector] private int barrierIncreasedPerRound;
    private HexagonalGrid _assignedGrid;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TileControlUI populationAdjustmentControlUI;
    [SerializeField] private TileControlUI kadirAdjustmentControlUI;
    [SerializeField] private InGameCanvasScript canvas;
    [ShowInInspector] private int _level;

    private SelectionManager _selectionManager;
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
            if (_selectionManager.AreThereAnySelectedCard())
            {
                if (_selectionManager.GetSelectedCard()._name==_name)
                {
                    Upgrade();
                    Destroy(_selectionManager.GetSelectedCard());
                    return;
                }    
            }
            
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
        this._sprites = cardDataSo.sprites;
        this._constructionRequirement = new Resources(cardDataSo.ConstructionRequirement);
        this._operationalRequirement = new Resources(cardDataSo.OperationalRequirement);
        this._outcomeResources = new Resources(cardDataSo.OutcomeResources);
        this._startingBarrier = cardDataSo.startingBarrier;
        _level = 1;
        spriteRenderer.sprite = _sprites[_level-1];
    }

    private void Upgrade()
    {
        _level = 2;
        spriteRenderer.sprite = _sprites[_level-1];
        _outcomeResources.Multiply(_level);
    }

    public void Play(HexagonalGrid hexagonalGrid)
    {
        transform.position = hexagonalGrid.transform.position;
        EventManager.OnConsumeResource(_constructionRequirement);
        EventManager.OnCardPlayed(this);
        if (_startingBarrier!=0)
        {
            EventManager.OnBarrierIncreased(_startingBarrier);
        }
        _assignedGrid = hexagonalGrid;

    }

    private void GenerateOutCome(Resources currentResources)
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

                if (barrierIncreasedPerRound!=0)
                {
                    EventManager.OnBarrierIncreased(barrierIncreasedPerRound);
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

    public void AssignSelectionManager(SelectionManager selectionManager)
    {
        _selectionManager = selectionManager;
    }
}
