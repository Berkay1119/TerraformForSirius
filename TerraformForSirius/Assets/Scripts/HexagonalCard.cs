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
    public List<CardTypes> typesToGiveBonus;
    public List<CardTypes> typesToTakeBonus;
    public List<CardTypes> typesToGiveNegative;
    public List<CardTypes> typesToTakeNegative;
    [ShowInInspector] private int _startingBarrier;
    [ShowInInspector] private int barrierIncreasedPerRound;
    private HexagonalGrid _assignedGrid;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TileControlUI populationAdjustmentControlUI;
    [SerializeField] private TileControlUI kadirAdjustmentControlUI;
    [SerializeField] private InGameCanvasScript canvas;
    [ShowInInspector] private int _level;
    
    private SelectionManager _selectionManager;
    private CardTypes _cardType;
    public int bonusCount;
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
                    if (_level==3)
                    {
                        return;
                    }
                    _selectionManager.SetSelectedCard(null);
                    Upgrade();
                    Destroy(_selectionManager.GetSelectedCard().gameObject);
                    EventManager.OnUpgrade(_selectionManager.GetSelectedCard());
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

        EventManager.OnCardSelected(_selectionManager.GetSelectedCard() == this ? null : this);
    }

    public void AssignData(CardDataSO cardDataSo)
    {
        this._name = cardDataSo.name;
        this._sprites = cardDataSo.sprites;
        this._constructionRequirement = new Resources(cardDataSo.ConstructionRequirement);
        this._operationalRequirement = new Resources(cardDataSo.OperationalRequirement);
        this._outcomeResources = new Resources(cardDataSo.OutcomeResources);
        this._startingBarrier = cardDataSo.startingBarrier;
        typesToGiveBonus = cardDataSo.typesToGiveBonus;
        typesToTakeBonus = cardDataSo.typesToTakeBonus;
        typesToTakeNegative = cardDataSo.typesToTakeNegative;
        typesToGiveNegative = cardDataSo.typesToGiveNegative;
        _cardType = cardDataSo.cardType;
        _level = 1;
        spriteRenderer.sprite = _sprites[_level-1];
    }

    private void Upgrade()
    {
        _level += 1;
        spriteRenderer.sprite = _sprites[_level-1];
        _outcomeResources.Multiply(_level);
        EventManager.OnResourceUpdated();
    }

    public void Play(HexagonalGrid hexagonalGrid)
    {
        GetComponent<CardHovering>().StopTweens();
        transform.position = hexagonalGrid.transform.position+new Vector3(0,0,-0.5f);
        EventManager.OnConsumeResource(_constructionRequirement);
        _assignedGrid = hexagonalGrid;
        EventManager.OnCardPlayed(this);
        if (_startingBarrier!=0)
        {
            EventManager.OnBarrierIncreased(_startingBarrier);
        }
        EventManager.OnResourceUpdated();
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
                else
                {
                    currentResources.Consume(_operationalRequirement);
                }

                if (barrierIncreasedPerRound!=0)
                {
                    if (_cardType==CardTypes.Protection)
                    {
                        EventManager.OnBarrierIncreased(barrierIncreasedPerRound+bonusCount);
                    }
                    else
                    {
                        EventManager.OnBarrierIncreased(barrierIncreasedPerRound);
                    }
                    
                }

                Resources resourcesToGenerate = new Resources(_outcomeResources);
                switch (_cardType)
                {
                    case CardTypes.Mine:
                        resourcesToGenerate.Mine += bonusCount;
                        break;
                    case CardTypes.Settlement:
                        resourcesToGenerate.Population += bonusCount;
                        break;
                    case CardTypes.GreenHouse:
                        resourcesToGenerate.Food += bonusCount;
                        break;
                    case CardTypes.WaterTank:
                        resourcesToGenerate.Water += bonusCount;
                        break;
                    default:
                        break;
                }
                
                currentResources.Generate(resourcesToGenerate);
            }
        }
        EventManager.OnResourceUpdated();
    }

    public bool IsCardPlaced()
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

    public CardTypes GetCardType()
    {
        return _cardType;
    }

    public HexagonalGrid GetAssignedGrid()
    {
        return _assignedGrid;
    }



    public List<CardTypes> GetTilesToGiveBonus()
    {
        return typesToGiveBonus;
    }
    
    public List<CardTypes> GetTilesToTakeBonus()
    {
        return typesToTakeBonus;
    }
}
