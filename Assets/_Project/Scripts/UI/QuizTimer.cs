using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class QuizTimer : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private UnityEvent _onFinishTime;
    [SerializeField] private Image _timerImage;
    [SerializeField] private TextMeshProUGUI _text;
    private float _currentTime;
    private bool _countingTime;

    void Start()
    {
        DOTween.Init();
    }

    public void StartTimer()
    {
        StopTimer();
        _countingTime = true;
        _currentTime = _time;
        _timerImage.fillAmount = 1;
        StartCoroutine(CountTime());
        _timerImage.DOFillAmount(0, _time).OnComplete(
            () => {
                _onFinishTime?.Invoke();
                StopTimer();
                });
    }

    public void StopTimer()
    {
        _timerImage.DOKill();
        _countingTime = false;
        _currentTime = _time;
        _text.text = _time.ToString();
        _timerImage.fillAmount = 1;
        StopAllCoroutines();
    }

    public IEnumerator CountTime()
    {
        while(_countingTime)
        {
            yield return new WaitForSeconds(1);
            _currentTime--;
            _text.text = _currentTime.ToString();
        }
    }
}
