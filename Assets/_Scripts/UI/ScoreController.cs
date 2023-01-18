using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreController : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    
    // Private Variables
    private TextMeshProUGUI _txtScore;

    private Vector3 _defScale;
    private int _collectedCollectableCount;
    
    #endregion Variables
    
    private void Awake()
    {
        _defScale = transform.localScale;
        _txtScore = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        CollectableGenerator.OnCollectableCollected += CollectableCollected;
    }

    private void CollectableCollected()
    {
        _collectedCollectableCount++;

        _txtScore.text = _collectedCollectableCount.ToString();

        PlayAnim();
    }

    private void PlayAnim()
    {
        transform.DOKill();

        transform.localScale = _defScale;

        transform.DOPunchScale(Vector3.one * .8f, .3f, 10, 1.5f);
    }
}
