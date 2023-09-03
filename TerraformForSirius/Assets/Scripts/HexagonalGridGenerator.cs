using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class HexagonalGridGenerator : MonoBehaviour
{
    [SerializeField] private int rowCount;
    [SerializeField] private int columnCount;
    [SerializeField] private GameObject hexagonalGridPrefab;
    [SerializeField] private float outerCircleRadius;

    [SerializeField] private Tuple<int, int> _openGrid = new Tuple<int, int>(4,4);
    
    [SerializeField] private SelectionManager selectionManager;

    private Dictionary<HexagonalGrid, HexagonalCoordinates> _hexagonalCoordinatesMap =
        new Dictionary<HexagonalGrid, HexagonalCoordinates>();
    
    private Dictionary<HexagonalCoordinates, HexagonalGrid> _hexagonalCoordinatesMapReverse =
        new Dictionary<HexagonalCoordinates, HexagonalGrid>();
    
    List<Tuple<int, int>> adjacentCoordinateDifferencesEven = new List<Tuple<int, int>>
    {
        new (1, 0),
        new (0, 1),
        new (-1, -1),
        new (-1, 0),
        new (0, -1),
        new (1, -1)
            
    };
    
    List<Tuple<int, int>> adjacentCoordinateDifferencesOdd = new List<Tuple<int, int>>
    {
        new (-1, 0),
        new (0, -1),
        new (1, 0),
        new (1, 1),
        new (0, 1),
        new (-1, 1)
            
    };

    private void OnEnable()
    {
        EventManager.CardPlayed += AffectAdjacentTiles;
        EventManager.CardPlayed += ExploreAdjacentGrids;
    }

    private void ExploreAdjacentGrids(HexagonalCard x)
    {
        List<HexagonalGrid> adjacentGrids = new List<HexagonalGrid>();
        
        HexagonalCoordinates currentCoordinates=_hexagonalCoordinatesMap[x.GetAssignedGrid()];

        List<Tuple<int, int>> adjacentCoordinateDifferences = currentCoordinates.X % 2 == 0
            ? adjacentCoordinateDifferencesEven
            : adjacentCoordinateDifferencesOdd;
        
        foreach (var tuple in adjacentCoordinateDifferences)
        {
            foreach (var pair in _hexagonalCoordinatesMapReverse)
            {
                if (pair.Key.X==new HexagonalCoordinates(currentCoordinates.X+tuple.Item1,currentCoordinates.Y+tuple.Item2).X)
                {
                    if (pair.Key.Y==new HexagonalCoordinates(currentCoordinates.X+tuple.Item1,currentCoordinates.Y+tuple.Item2).Y)
                    {
                        adjacentGrids.Add(pair.Value);
                    }
                    
                }
            }
        }

        foreach (var grid in adjacentGrids)
        {
            grid.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        EventManager.CardPlayed -= AffectAdjacentTiles;
        EventManager.CardPlayed -= ExploreAdjacentGrids;
    }


    private void Start()
    {
        GenerateGrids();
    }
    
    private void GenerateGrids()
    {
        void Generate(int startingColumnNumber)
        {
            // If startingColumnNumber is even, even columns will be generated.
            for (int i = startingColumnNumber; i < columnCount; i+=2)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    HexagonalGrid currentGrid = Instantiate(hexagonalGridPrefab,transform).GetComponent<HexagonalGrid>();
                    Vector3 startingLocation = startingColumnNumber == 1 ? transform.position+new Vector3(1.5f, -Mathf.Sqrt(3) / 2, 0) : transform.position+Vector3.zero;
                    currentGrid.transform.position = startingLocation + new Vector3((i/2)*outerCircleRadius*3f,-j*outerCircleRadius*Mathf.Sqrt(3), 0);
                    HexagonalCoordinates currentCoordinate = new HexagonalCoordinates(i, j);
                    _hexagonalCoordinatesMap.Add(currentGrid,currentCoordinate);
                    currentGrid.AssignCoordinate(currentCoordinate);
                    currentGrid.AssignSelectionManager(selectionManager);
                }
            }
        }
        
        Generate(0);
        Generate(1);

        foreach (var pair in _hexagonalCoordinatesMap)
        {
            pair.Key.gameObject.SetActive(false);
            if (pair.Value.X==_openGrid.Item1 && pair.Value.Y == _openGrid.Item2)
            {
                pair.Key.gameObject.SetActive(true);
            }
            _hexagonalCoordinatesMapReverse.Add(pair.Value,pair.Key);
        }
    }

    public List<HexagonalGrid> GetEdgeTileList()
    {
        List<HexagonalGrid> list = new List<HexagonalGrid>();
        foreach (var pair in _hexagonalCoordinatesMap)
        {
            //if (pair.Value.X==0 || pair.Value.X==columnCount-1)
            if(pair.Key.GetPlacedCard()!=null)
            {
                list.Add(pair.Key);
            }
        }
        return list;
    }

    private void AffectAdjacentTiles(HexagonalCard hexagonalCard)
    {
        List<HexagonalCard> adjacentGrids = new List<HexagonalCard>();
        
        HexagonalCoordinates currentCoordinates=_hexagonalCoordinatesMap[hexagonalCard.GetAssignedGrid()];

        
        List<Tuple<int, int>> adjacentCoordinateDifferences = currentCoordinates.X % 2 == 0
            ? adjacentCoordinateDifferencesEven
            : adjacentCoordinateDifferencesOdd;
        foreach (var tuple in adjacentCoordinateDifferences)
        {
            foreach (var pair in _hexagonalCoordinatesMapReverse)
            {
                if (pair.Key.X==new HexagonalCoordinates(currentCoordinates.X+tuple.Item1,currentCoordinates.Y+tuple.Item2).X)
                {
                    if (pair.Key.Y==new HexagonalCoordinates(currentCoordinates.X+tuple.Item1,currentCoordinates.Y+tuple.Item2).Y)
                    {
                        adjacentGrids.Add(pair.Value.GetPlacedCard());
                    }
                    
                }
            }
            
        }

        foreach (var card in adjacentGrids)
        {
            if (card==null)
            {
                continue;
            }

            if (hexagonalCard.GetTilesToGiveBonus().Contains(card.GetCardType()))
            {
                card.bonusCount++;
            }

            if (hexagonalCard.typesToGiveNegative.Contains(card.GetCardType()))
            {
                card.bonusCount--;
            }
        }
        
        foreach (var card in adjacentGrids)
        {
            if (card==null)
            {
                continue;
            }

            if (hexagonalCard.GetTilesToTakeBonus().Contains(card.GetCardType()))
            {
                hexagonalCard.bonusCount++;
            }

            if (hexagonalCard.typesToTakeNegative.Contains(card.GetCardType()))
            {
                hexagonalCard.bonusCount--;
            }
        }
    }
}


[Serializable]
public class HexagonalCoordinates
{
    public int X;
    public int Y;

    public HexagonalCoordinates(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override bool Equals(object o)
    {
        if (X==(((HexagonalCoordinates)o)!).X && Y==(((HexagonalCoordinates)o)!).Y )
        {
            return true;
        }

        return false;
    }

}

