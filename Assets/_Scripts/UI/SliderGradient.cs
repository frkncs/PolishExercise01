using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGradient : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    
    // Private Variables
    [SerializeField] private Gradient sliderGradient;
    [SerializeField] private Image sliderFillImage;

    private Slider _slider;
    
    #endregion Variables
    
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        
        _slider.value = 0;
    }

    private void Start()
    {
        TimerController.OnTimeDelaySetted += () =>
        {
            _slider.maxValue = TimerController.TimerDelay;
        };
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameStates.Lose)
        {
            return;
        }

        _slider.value = Mathf.Lerp(_slider.value, TimerController.TimerValue, 10 * Time.deltaTime);
    }

    public void OnSliderValueChanged()
    {
        var value = _slider.value / _slider.maxValue;
        
        sliderFillImage.color = sliderGradient.Evaluate(value);
    }
}
