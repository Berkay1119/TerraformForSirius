using Sirenix.OdinInspector;
using UnityEngine;

public class HexagonalGrid:MonoBehaviour
{
    [ShowInInspector]private HexagonalCoordinates _assignedCoordinate;

    public void AssignCoordinate(HexagonalCoordinates coordinates)
    {
        _assignedCoordinate = coordinates;
    }
}
