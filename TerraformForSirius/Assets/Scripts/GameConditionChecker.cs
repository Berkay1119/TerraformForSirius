using System;
using UnityEngine;

public class GameConditionChecker : MonoBehaviour
{
    [Header("Win Conditions")]
    [SerializeField] private int winPopulation = 1000;
    [SerializeField] private float winPlanetHealthPercentage = 0.75f; // 75%
    [SerializeField] private int winResourceCount = 500;
    [SerializeField] private int winTurnsCount = 5;

    [Header("Lose Conditions")]
    [SerializeField] private int losePopulationHigh = 1500;
    [SerializeField] private int losePopulationLow = 0;

    [Header("UI Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject questionMark;

    private RoundTracker roundTracker;
    private int turnsMaintainedWinConditions = 0;

    private void Start()
    {
        EventManager.CheckGameEnding += CheckConditions;
        roundTracker = FindObjectOfType<RoundTracker>();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.NextTurn -= CheckConditions;
    }

    public void CheckConditions()
    {
        Resources currentResources = roundTracker.GetCurrentResources();

        // Win Condition
        if(currentResources.Population >= winPopulation 
            && currentResources.PlanetHealth >= roundTracker.GetMaxPlanetHealth() * winPlanetHealthPercentage
            && currentResources.Water >= winResourceCount
            && currentResources.Food >= winResourceCount
            && currentResources.Mine >= winResourceCount)
        {
            turnsMaintainedWinConditions++;
            
            if(turnsMaintainedWinConditions >= winTurnsCount)
            {
                DisplayWinPanel();
                return; // Exit the function so lose conditions aren't checked after a win.
            }
        }
        else
        {
            // If any win condition breaks, reset the count.
            turnsMaintainedWinConditions = 0;
        }

        // Lose Conditions
        if(currentResources.Population >= losePopulationHigh
            || currentResources.Population <= losePopulationLow
            || (currentResources.Water == 0 && currentResources.Food == 0 && currentResources.Mine == 0) || roundTracker.GetCurrentBarrier()<0)
        {
            DisplayLosePanel();
        }
    }

    private void DisplayWinPanel()
    {
        winPanel.SetActive(true);
        questionMark.SetActive(false);
        // Additional logic for displaying the win panel if needed
    }

    private void DisplayLosePanel()
    {
        losePanel.SetActive(true);
        questionMark.SetActive(false);
        // Additional logic for displaying the lose panel if needed
    }
}
