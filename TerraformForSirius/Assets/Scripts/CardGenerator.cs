using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] private List<CardDataSO> _cardData = new List<CardDataSO>();
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private SelectionManager selectionManager;
    
    //Testing Purposes
    [SerializeField] private Transform createdCardTransform;
    
    private CardDataSO GetRandomCardData()
    {
        int randomIndex = Random.Range(0, _cardData.Count);
        return _cardData[randomIndex];
    }

    [Button]
    private void SpawnCard()
    {
        CreateCard(createdCardTransform.position);
    }
    
    public HexagonalCard CreateCard(Vector3 atPosition)
    {
        HexagonalCard hexagonalCard = Instantiate(cardPrefab).GetComponent<HexagonalCard>();
        hexagonalCard.AssignData(GetRandomCardData());
        hexagonalCard.transform.position = atPosition;
        hexagonalCard.AssignSelectionManager(selectionManager);
        return hexagonalCard;
    }
}
