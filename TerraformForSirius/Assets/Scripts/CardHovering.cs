using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardHovering : MonoBehaviour
{
    [SerializeField] private HexagonalCard card;
    [SerializeField] private float jumpAmount;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float hoverScale;
    [SerializeField] private float hoverSpeed;
    private Tween _jumpTween;
    private Vector3 _originalScale;
    private Vector3 _originalPosition;
    
    public void StarSequence()
    {
        _originalScale = transform.localScale;
        _originalPosition = transform.position;
        StartJumping();
    }

    private void OnMouseOver()
    {
        if (card.IsCardPlaced())
        {
            return;
        }
        HoverTheCard();
    }

    private void OnMouseExit()
    {
        if (card.IsCardPlaced())
        {
            return;
        }
        transform.localScale = _originalScale;
    }

    private void StartJumping()
    {
        transform.position = _originalPosition;
        _jumpTween=transform.DOMoveY(_originalPosition.y+jumpAmount, jumpSpeed).SetSpeedBased().SetLoops(-1,LoopType.Yoyo);
    }
    

    private void HoverTheCard()
    {
        transform.position = _originalPosition;
        transform.DOScale(hoverScale*_originalScale, hoverSpeed).SetSpeedBased();
    }

    public void StopTweens()
    {
        _jumpTween.Kill();
    }
}
