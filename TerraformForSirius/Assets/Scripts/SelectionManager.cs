using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectionManager : MonoBehaviour
{
    private HexagonalCard _selectedCard;
    [SerializeField] private RoundTracker roundTracker;

    private void OnEnable()
    {
        EventManager.CardSelected += AssignSelectedCard;
    }

    private void OnDisable()
    {
        EventManager.CardSelected -= AssignSelectedCard;
    }

    private void AssignSelectedCard(HexagonalCard x)
    {
        _selectedCard = x;
    }

    public bool AreThereAnySelectedCard()
    {
        return _selectedCard != null;
    }

    public void PlaceCard(HexagonalGrid hexagonalGrid)
    {
        _selectedCard.Play(hexagonalGrid);
        hexagonalGrid.FulFillTheArea(_selectedCard);
        _selectedCard = null;
    }

    public bool AreThereEnoughResources()
    {
        return roundTracker.AreConditionsSufficient(_selectedCard.GetResources());
    }
}
