
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "New Card Data")]
public class CardDataSO:ScriptableObject
{
    public bool isProtector;
    public string cardName;
    public Sprite sprite;
    public Resources ConstructionRequirement;
    public Resources OperationalRequirement;
    public Resources OutcomeResources;
    [ShowIf("isProtector")] public int startingBarrier=0;
    [ShowIf("isProtector")] public int perRoundBarrier;
}
