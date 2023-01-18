using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    public static Action OnTimeDelaySetted, OnLastSeconds;
    public static float TimerValue => _timer;
    public static float TimerDelay => _timeDelay;
    
    // Private Variables
    [SerializeField] private float waveTimerMultiplier = 5;
    
    private float _wave;

    private int _waveCount;
    private int _waveCountMultiplier;

    private bool _isTimerSFXPlayed;

    private static float _timer;
    private static float _timeDelay;
    
    #endregion Variables

    private void Start()
    {
        _timer = -1;
        _waveCountMultiplier = 1;
        
        CollectableGenerator.OnNewWaveStarted += () =>
        {
            _waveCount++;
            
            if (_waveCount + 1 > CollectableGenerator.IncreaseWaveCollectableTime * _waveCountMultiplier)
            {
                _waveCountMultiplier++;
                waveTimerMultiplier -= .5f;

                waveTimerMultiplier = Mathf.Max(waveTimerMultiplier, 1.5f);
            }
            
            _timeDelay = _waveCountMultiplier * waveTimerMultiplier;
            _timer = _timeDelay;
            _isTimerSFXPlayed = false;
            
            OnTimeDelaySetted?.Invoke();
        };
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameStates.Lose)
        {
            return;
        }

        if (_timer == -1)
        {
            return;
        }
        
        _timer -= Time.deltaTime;

        if (!_isTimerSFXPlayed && _timer <= TimerDelay * .2f)
        {
            OnLastSeconds?.Invoke();
            _isTimerSFXPlayed = true;
        }
        
        if (_timer <= 0)
        {
            GameManager.LoseGame?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnTimeDelaySetted = null;
        OnLastSeconds = null;
    }
}
