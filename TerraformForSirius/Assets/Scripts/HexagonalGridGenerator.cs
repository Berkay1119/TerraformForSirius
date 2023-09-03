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
    [SerializeField] private SelectionManager selectionManager;

    private Dictionary<HexagonalGrid, HexagonalCoordinates> _hexagonalCoordinatesMap =
        new Dictionary<HexagonalGrid, HexagonalCoordinates>();
    
    private Dictionary<HexagonalCoordinates, HexagonalGrid> _hexagonalCoordinatesMapReverse =
        new Dictionary<HexagonalCoordinates, HexagonalGrid>();

    private void OnEnable()
    {
        EventManager.CardPlayed += AffectAdjacentTiles;
    }

    private void OnDisable()
    {
        EventManager.CardPlayed -= AffectAdjacentTiles;
    }

    [Button]
    private void GenerateGrids()
    {
        void Generate(int startingColumnNumber)
        {
            // If startingColumnNumber is even, even columns will be generated.
            for (int i = startingColumnNumber; i < columnCount; i+=2)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    HexagonalGrid currentGrid = Instantiate(hexagonalGridPrefab).GetComponent<HexagonalGrid>();
                    Vector3 startingLocation = startingColumnNumber == 1 ? new Vector3(1.5f, -Mathf.Sqrt(3) / 2, 0) : Vector3.zero;
                    currentGrid.transform.position = startingLocation + new Vector3((i/2)*outerCircleRadius*3f,j*outerCircleRadius*Mathf.Sqrt(3), 0);
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
        List<Tuple<int, int>> adjacentCoordinateDifferences = new List<Tuple<int, int>>
        {
            new(0,-1),
            new(1,0),
            new(1,1),
            new(0,1),
            new(-1,1),
            new(-1,0)
            
        };
        HexagonalCoordinates currentCoordinates=_hexagonalCoordinatesMap[hexagonalCard.GetAssignedGrid()];

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

            if (hexagonalCard.typesToGiveNegative.Contains(card.GetCardType()))
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

