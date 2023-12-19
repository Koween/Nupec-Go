using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    [Serializable]
    private struct Interactions
    {
        public Transform TooltipHandPosition;
        public Transform TooltipMessagePosition;
        public screenInteractions ScreenInteraction;
        public float RequiredDuration;
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
    [SerializeField] private List<Interactions> _interactions;
    private screenInteractions _currentInteraction;

    public void Awake()
    {
        if(_tooltipHand == null)
        _tooltipHand = GameObject.FindGameObjectWithTag("tooltipHand");
        if(_tooltipMessage == null)
        _tooltipMessage = GameObject.FindGameObjectWithTag("tooltipMessage");
    }

    public void StartInteraction(screenInteractions interaction)
    {
        int interactionIndex = GetInteractionIndex(interaction);
        if(interactionIndex == -1)
        { 
            Debug.LogError("The interactive object doesn't cotains the requested interaction");
            return;
        }
        _currentInteraction = interaction;
        _tooltipHand.transform.position = _interactions[interactionIndex].TooltipHandPosition.position;
        _tooltipHand.transform.rotation = _interactions[interactionIndex].TooltipHandPosition.rotation;
        _tooltipMessage.transform.position = _interactions[interactionIndex].TooltipMessagePosition.position;
        _tooltipMessage.GetComponent<Animator>().SetTrigger("SpawnMessage");
        _tooltipHand.GetComponent<Animator>().SetBool($"Waitting{interaction.ToString()}Interaction", true);
        ScreenInteractionSubscriptionHandler(true);
    }

    public void FinishInteraction()
    {
        _tooltipMessage.GetComponent<Animator>().SetTrigger("HideMessage");
        _tooltipHand.GetComponent<Animator>().SetBool("WaittingInteraction", false);
        ScreenInteractionSubscriptionHandler(false);
    }

    public int GetInteractionIndex(screenInteractions requestedInteraction)
    {
        int i = 0;
        foreach (var interaction in _interactions)
        {
            if(interaction.ScreenInteraction == requestedInteraction)
                return i;
            i++;
        }
        return -1;
    }

    private void ScreenInteractionSubscriptionHandler(bool subscribe)
    {
        switch (_currentInteraction)
        {
            case screenInteractions.Touch:
            if(subscribe)
                ScreenInteractions.Instance.OnTouchEvent.AddListener(FinishInteraction);
            else
                ScreenInteractions.Instance.OnTouchEvent.RemoveListener(FinishInteraction);
            break;
            case screenInteractions.DragSideToSide: 
            if(subscribe)
                ScreenInteractions.Instance.OnChnageDragDirectionEvent.AddListener(FinishInteraction);
            else
                ScreenInteractions.Instance.OnChnageDragDirectionEvent.RemoveListener(FinishInteraction);
            break;
            case screenInteractions.DragDownToUp:
            if(subscribe)
                ScreenInteractions.Instance.OnDragDownToUp.AddListener(FinishInteraction);
            else
                ScreenInteractions.Instance.OnDragDownToUp.RemoveListener(FinishInteraction);
            break;
            case screenInteractions.Swipe:
            if(subscribe)
                ScreenInteractions.Instance.OnSwipe.AddListener(FinishInteraction);
            else
                ScreenInteractions.Instance.OnSwipe.RemoveListener(FinishInteraction);
            break;
        }
    }

}
