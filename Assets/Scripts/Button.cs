using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField] private float _healthChangeValue;
    [SerializeField] private HealthChanger _healthChanger;

    public void OnClik()
    {
        _healthChanger.ChangeValue(_healthChangeValue);
    }
    
}
