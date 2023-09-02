using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InGameCanvasScript : MonoBehaviour
{
    public bool IsPointerOverGameObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
