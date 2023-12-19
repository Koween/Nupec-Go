using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]

public class UIAnimEvents : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private UnityEvent _onFadeIn;
    private UnityEvent _onFadeOut;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        //Todo: usar Dotween para fade.
        _canvasGroup.alpha = 1;
        _onFadeIn?.Invoke();
    }

    private void FadeOut()
    {
        //Todo: usar Dotween para fade.
        _canvasGroup.alpha = 0;
        _onFadeOut?.Invoke();
    }

}
