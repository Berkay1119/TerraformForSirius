using System;
using UnityEngine;
public class SelectionManager : MonoBehaviour
{
    private HexagonalCard _selectedCard;


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
    }
}
