using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnScript : MonoBehaviour
{
    public void OnNextTurnPressed()
    {
        EventManager.OnNextTurn();
    }
}
