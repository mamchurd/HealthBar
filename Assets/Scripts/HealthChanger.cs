using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthChanger : MonoBehaviour
{

    [SerializeField] private Slider _slider;
    
    private float _maxValue;
    private float _maxTime;
    private Coroutine _coroutine;
    private Queue<float> _healChangesValues;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _healChangesValues = new Queue<float>();

        _maxValue = _slider.maxValue;
        _maxTime = 5;
    }

    private void Update()
    {
        if (_coroutine == null && _healChangesValues.Count != 0)
        {
            float healthChangeValue = _healChangesValues.Dequeue();

            float normalizedValue = Math.Abs(healthChangeValue) / _maxValue;
            float duration = normalizedValue * _maxTime;

            _coroutine = StartCoroutine(LerpValue(_slider.value, _slider.value + healthChangeValue, duration));
        }
    }

    public void ChangeValue(float value)
    {
        _healChangesValues.Enqueue(value);
    }

    private IEnumerator LerpValue(float startValue, float endValue, float duration)
    {
        float elapsed = 0;
        float nextValue;

        while (elapsed < duration)
        {
            nextValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            SetSliderValue(nextValue);
            elapsed += Time.deltaTime;
            yield return null;
        }

        SetSliderValue(endValue);

        _coroutine = null;
    }

    private void SetSliderValue(float value)
    {
        _slider.value = value;
    }
}
