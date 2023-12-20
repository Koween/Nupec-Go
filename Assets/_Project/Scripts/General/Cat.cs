using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cat : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFinishAnimation;
    
    public void OnFinishAnimation()
    {
        _onFinishAnimation?.Invoke();
    }
}
