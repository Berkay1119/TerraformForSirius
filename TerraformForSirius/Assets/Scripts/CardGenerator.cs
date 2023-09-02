using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] private List<CardDataSO> _cardData = new List<CardDataSO>();
    [SerializeField] private GameObject cardPrefab;
    
    private CardDataSO GetRandomCardData()
    {
        int randomIndex = Random.Range(0, _cardData.Count);
        return _cardData[randomIndex];
    }

    public HexagonalCard CreateCard(Vector3 atPosition)
    {
        HexagonalCard hexagonalCard = Instantiate(cardPrefab).GetComponent<HexagonalCard>();
        hexagonalCard.AssignData(GetRandomCardData());
        return hexagonalCard;
    }
}
