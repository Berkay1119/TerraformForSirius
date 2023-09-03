using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private CardGenerator cardGenerator;

    [SerializeField]
    private List<Transform> spawnLocations = new List<Transform>();
    private Dictionary<HexagonalCard, Transform> cardsInPlay = new Dictionary<HexagonalCard, Transform>();

    private void Start()
    {
        foreach (Transform location in spawnLocations)
        {
            SpawnCardAtLocation(location);
        }
        EventManager.CardPlayed += CardUsed;
        EventManager.Upgrade += CardUsed;
    }

    private void OnDestroy()
    {
        EventManager.CardPlayed -= CardUsed;
        EventManager.Upgrade -= CardUsed;
    }

    private void SpawnCardAtLocation(Transform location)
    {
        HexagonalCard newCard = cardGenerator.CreateCard(location.position);
        cardsInPlay[newCard] = location;
        newCard.GetComponent<CardHovering>().StarSequence();
    }

    private void CardUsed(HexagonalCard usedCard)
    {
        if (cardsInPlay.ContainsKey(usedCard))
        {
            Transform locationOfUsedCard = cardsInPlay[usedCard];
            cardsInPlay.Remove(usedCard);
            SpawnCardAtLocation(locationOfUsedCard);
        }
    }
}
