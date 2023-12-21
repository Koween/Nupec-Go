using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cat : MonoBehaviour
{
    [SerializeField] private UnityEvent 
    _onFinishPeeingAnimation, _onFinishInapetenciaAnimation, _onFinishColicoAnimation, _onFinishSkinAnimation, _onFinishAnimation;

    public void OnFinishPeeingAnimation()
    {
        _onFinishPeeingAnimation?.Invoke();
    }
    public void OnFinishInapetenciaAnimation()
    {
        _onFinishInapetenciaAnimation?.Invoke();
    }
    public void OnFinishColicoAnimation()
    {
        _onFinishColicoAnimation?.Invoke();
    }

    public void OnFinishSkinAnimation()
    {
        _onFinishSkinAnimation?.Invoke();
    }

    public void OnFinishAnimation()
    {
        _onFinishAnimation?.Invoke();
    }
}
