using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HexagonalGridGenerator : MonoBehaviour
{
    [SerializeField] private int rowCount;
    [SerializeField] private int columnCount;
    [SerializeField] private GameObject hexagonalGridPrefab;
    [SerializeField] private float outerCircleRadius;

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
                    currentGrid.transform.position = startingLocation + new Vector3(j*outerCircleRadius*Mathf.Sqrt(3), i*outerCircleRadius*3, 0);
                    HexagonalCoordinates currentCoordinate = new HexagonalCoordinates(i, j);
                    _hexagonalCoordinatesMap.Add(currentGrid,currentCoordinate);
                    currentGrid.AssignCoordinate(currentCoordinate);
                }
            }
        }
        
        Generate(0);
        Generate(1);
    }
}


public class HexagonalCoordinates
{
    public int X;
    public int Y;

    public HexagonalCoordinates(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override bool Equals(object obj)
    {
        //NeedT
        return base.Equals(obj);
        
    }
}

