using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class HexagonalGrid:MonoBehaviour
{
    [ShowInInspector]private HexagonalCoordinates _assignedCoordinate;
    private SelectionManager _selectionManager;
    private HexagonalCard _placedCard;

    public void AssignCoordinate(HexagonalCoordinates coordinates)
    {
        _assignedCoordinate = coordinates;
        
    }

    public void AssignSelectionManager(SelectionManager selectionManager)
    {
        _selectionManager = selectionManager;
    }

    private void OnMouseDown()
    {
        
        if (_selectionManager.AreThereAnySelectedCard())
        {
            if (!_selectionManager.AreThereEnoughResources())
            {
                return;
            }
            _selectionManager.PlaceCard(this);
        }
    }

    public void FulFillTheArea(HexagonalCard hexagonalCard)
    {
        _placedCard = hexagonalCard;
    }
}
