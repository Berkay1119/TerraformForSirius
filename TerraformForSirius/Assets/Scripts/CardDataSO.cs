
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class CardDataSO:ScriptableObject
{
    public bool isProtector;
    public string name;
    public Sprite sprite;
    public Resources ConstructionRequirement;
    public Resources OperationalRequirement;
    public Resources OutcomeResources;
    [ShowIf("isProtector")] public int barrier=0;
}
