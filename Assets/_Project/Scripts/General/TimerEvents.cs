using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerEvents : MonoBehaviour
{

    [SerializeField] private float _time;
    [SerializeField] private UnityEvent _onFinishTime;
    private float _currentTime;
    private bool _countingTime = true;

    public void Update()
    {
        if(!_countingTime) return;
        _currentTime += Time.deltaTime;
        if(_currentTime >= _time)
        {
            _onFinishTime?.Invoke();
            _countingTime = false;
        }
    }
}
