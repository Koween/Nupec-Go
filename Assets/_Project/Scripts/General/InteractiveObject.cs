using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    [Serializable]
    private struct Interactions
    {
        public Transform TooltipHandPosition;
        public Transform TooltipMessagePosition;
        [TextArea] public string TooltipMessageText;
        public screenInteractions ScreenInteraction;
        public float RequiredSeconds;
    }


    public enum screenInteractions
    {
        Touch,
        Swipe,
        DragDownToUp,
        DragSideToSide
    }

    private static GameObject _tooltipMessage;
    private static GameObject _tooltipHand;
    private screenInteractions _currentScreenInteraction;
    private int _currenInteractionIndex;
    public bool InteractionEnable{ get; set;}
    [SerializeField] private List<Interactions> _interactions;
    [SerializeField] private UnityEvent _onFinishCurrentInteraction;

    public void Awake()
    {
        if(_tooltipHand == null)
        _tooltipHand = GameObject.FindGameObjectWithTag("TooltipHand");
        if(_tooltipMessage == null)
        _tooltipMessage = GameObject.FindGameObjectWithTag("TooltipMessage");
    }

    public void StartInteraction(string interaction)
    {
        _currenInteractionIndex = GetInteractionIndex(interaction);
        if(_currenInteractionIndex == -1)
        { 
            Debug.LogError("The interactive object doesn't cotains the requested interaction");
            return;
        }
        _currentScreenInteraction = _interactions[_currenInteractionIndex].ScreenInteraction;
        _tooltipHand.transform.position = _interactions[_currenInteractionIndex].TooltipHandPosition.position;
        _tooltipHand.transform.rotation = _interactions[_currenInteractionIndex].TooltipHandPosition.rotation;
        _tooltipMessage.transform.position = _interactions[_currenInteractionIndex].TooltipMessagePosition.position;
        _tooltipMessage.GetComponent<TooltipMessage>().SetMessage(_interactions[_currenInteractionIndex].TooltipMessageText);

        _tooltipMessage.GetComponent<Animator>().SetTrigger("FadeIn");
        _tooltipHand.GetComponent<Animator>().SetBool(interaction, true);
        ScreenInteractionSubscriptionHandler(true);
    }

    public void FinishInteraction()
    {
        if(!InteractionEnable) return;
        _tooltipMessage.GetComponent<Animator>().SetTrigger("FadeOut");
        _tooltipHand.GetComponent<Animator>().SetBool(_currentScreenInteraction.ToString(), false);
        ScreenInteractionSubscriptionHandler(false);
        _onFinishCurrentInteraction?.Invoke();
    }

    public int GetInteractionIndex(string requestedInteraction)
    {
        int i = 0;
        foreach (var interaction in _interactions)
        {
            if(interaction.ScreenInteraction.ToString() == requestedInteraction)
                return i;
            i++;
        }
        return -1;
    }

    private void ScreenInteractionSubscriptionHandler(bool subscribe)
    {
        switch (_currentScreenInteraction)
        {
            case screenInteractions.Touch:
            if(subscribe)
                ScreenInteractions.Instance.OnTouchEvent.AddListener(CheckElapsedInteractionTime);
            else
                ScreenInteractions.Instance.OnTouchEvent.RemoveListener(CheckElapsedInteractionTime);
            break;
            case screenInteractions.DragSideToSide: 
            if(subscribe)
                ScreenInteractions.Instance.OnChnageDragDirectionEvent.AddListener(CheckElapsedInteractionTime);
            else
                ScreenInteractions.Instance.OnChnageDragDirectionEvent.RemoveListener(CheckElapsedInteractionTime);
            break;
            case screenInteractions.DragDownToUp:
            if(subscribe)
                ScreenInteractions.Instance.OnDragDownToUp.AddListener(CheckElapsedInteractionTime);
            else
                ScreenInteractions.Instance.OnDragDownToUp.RemoveListener(CheckElapsedInteractionTime);
            break;
            case screenInteractions.Swipe:
            if(subscribe)
                ScreenInteractions.Instance.OnSwipe.AddListener(FinishInteraction);
            else
                ScreenInteractions.Instance.OnSwipe.RemoveListener(FinishInteraction);
            break;
        }
    }

    private void CheckElapsedInteractionTime(float elapsedTime)
    {
        if(_interactions[_currenInteractionIndex].RequiredSeconds <= elapsedTime)
        FinishInteraction();
    }

}
