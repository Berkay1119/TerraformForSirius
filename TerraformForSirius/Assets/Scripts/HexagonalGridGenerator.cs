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
}

