using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplaymentUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    public void DisplayInfo(string str)
    {
        _textMeshProUGUI.text = str;
    }
}
