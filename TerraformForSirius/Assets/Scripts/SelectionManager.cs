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
}
