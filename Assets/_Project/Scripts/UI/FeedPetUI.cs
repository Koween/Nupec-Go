using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FeedPetUI : MonoBehaviour
{
    [SerializeField] private Animator _foodAnimator;
    [SerializeField] private Image _foodBarFill;
    [SerializeField] private GameObject _feedInstructions, _arrow;
    [SerializeField] private UnityEvent _onFoodBarIsFull;
    [SerializeField] private bool _isPlayingFeedAnimation;
    [SerializeField] private Color _emptyBarColor, _normalBarColor, _fullBarColor;

    private void Awake()
    {
        ScreenInteractions.Instance.OnSwipe.AddListener(ThrowFood);
        ScreenInteractions.Instance.UseLayerMask = false;
        ScreenInteractions.Instance.ChangeSortOrder(2);
    }

    private void ThrowFood()
    {
        if(!_isPlayingFeedAnimation)
        {
            _isPlayingFeedAnimation = true;
            _foodAnimator.Play("Feed");
        }
    }

//This method is called by the animation event of the animation "feed" in the last frame
    public void OnFeedPet()
    {
        _feedInstructions.SetActive(false);
        //_arrow.SetActive(false);
        _foodBarFill.DOFillAmount(_foodBarFill.fillAmount += .20f, .3f)
        .OnComplete(() => SetBarColor());

    }

    public void SetBarColor()
    {
        if(_foodBarFill.fillAmount  < 0.5f)
            _foodBarFill.DOColor(_emptyBarColor, .1f).OnComplete(() => OnChangeColor());
        if(_foodBarFill.fillAmount < 1 && _foodBarFill.fillAmount >= 0.5)
            _foodBarFill.DOColor(_normalBarColor, .1f).OnComplete(() => OnChangeColor());
        if(_foodBarFill.fillAmount == 1)
           _foodBarFill.DOColor(_fullBarColor, .1f).OnComplete(() => OnChangeColor());
    }

    private void OnChangeColor()
    {
        if(_foodBarFill.fillAmount == 1) 
                StartCoroutine(FinishFeedInteractionDelay());
        else
        _isPlayingFeedAnimation = false;
        _foodAnimator.Play("FeedIdle");
    }

    private IEnumerator FinishFeedInteractionDelay()
    {
        yield return new WaitForSeconds(1);
        _onFoodBarIsFull?.Invoke();
    }
}
