using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private CardGenerator cardGenerator;

    [SerializeField]
    private List<Transform> spawnLocations = new List<Transform>();

    // This dictionary will keep track of cards in play and their corresponding spawn location.
    private Dictionary<HexagonalCard, Transform> cardsInPlay = new Dictionary<HexagonalCard, Transform>();

    private void Start()
    {
        // Initial spawn of cards at game start.
        foreach (Transform location in spawnLocations)
        {
            SpawnCardAtLocation(location);
        }

        // Listen to card selected event to determine if a card has been played.
        EventManager.CardPlayed += CardUsed;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks.
        EventManager.CardPlayed -= CardUsed;
    }

    private void SpawnCardAtLocation(Transform location)
    {
        HexagonalCard newCard = cardGenerator.CreateCard(location.position);
        cardsInPlay[newCard] = location;
    }

    private void CardUsed(HexagonalCard usedCard)
    {
        // Check if the used card is in our dictionary.
        if (cardsInPlay.ContainsKey(usedCard))
        {
            // Get the location where the card was before being used.
            Transform locationOfUsedCard = cardsInPlay[usedCard];

            // Remove the used card from our dictionary.
            cardsInPlay.Remove(usedCard);

            // Destroy the used card (or you can disable it or move it to a different location depending on your game's logic).
            
            
            //Destroy(usedCard.gameObject);

            // Spawn a new card at the same location.
            SpawnCardAtLocation(locationOfUsedCard);
        }
    }
}
