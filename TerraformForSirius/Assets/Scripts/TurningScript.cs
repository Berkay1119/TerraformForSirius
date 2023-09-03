using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class TurningScript : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToTransform;
    [SerializeField] private float turnDegreePerTurn=90f;
    [SerializeField] private float totalTurnSecond = 3f;
    [SerializeField] private Ease ease;

    private void OnEnable()
    {
        EventManager.NextTurn += Turn;
    }

    private void OnDisable()
    {
        EventManager.NextTurn -= Turn;
    }

    private void Turn()
    {
        gameObjectToTransform.transform.DORotate(transform.rotation.eulerAngles+new Vector3(0,0,turnDegreePerTurn), totalTurnSecond).SetEase(ease);
    }
}
