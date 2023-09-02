using UnityEngine;

public class HexagonalGrid:MonoBehaviour
{
    private HexagonalCoordinates _assignedCoordinate;

    public void AssignCoordinate(HexagonalCoordinates coordinates)
    {
        _assignedCoordinate = coordinates;
    }
}
