using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScreenToWorldRaycast))]
public class ScreenInteractions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    
    private Vector2 _dragStartPosition;
    private Vector2 _dragPreviousPosition;
    private Vector2 _lastDragDirection;
    public UnityEvent OnSwipe;
    public bool DoingDrag {get; private set;}
    public UnityEvent OnTouchEvent, OnChnageDragDirectionEvent, OnDragDownToUp;
    public static ScreenInteractions Instance;
    private ScreenToWorldRaycast _screenToWorldRaycast;
    private LayerMask layerMask = LayerMask.NameToLayer("InteractiveObject");

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            _screenToWorldRaycast = GetComponent<ScreenToWorldRaycast>();
        }
        else 
            Destroy(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_screenToWorldRaycast.ThrowRayScreenToWorld(eventData.position, layerMask))
        OnTouchEvent?.Invoke();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragStartPosition = eventData.position;
        _dragPreviousPosition = _dragStartPosition;
        DoingDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.position != _dragPreviousPosition)
            _dragPreviousPosition = eventData.position;
        var currentDirection = GetDragDirection();
        if(_lastDragDirection != currentDirection)
        {
            _lastDragDirection = currentDirection;
            OnChnageDragDirectionEvent?.Invoke();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DoingDrag = false;
        OnSwipe?.Invoke();
        Vector2 direction = GetDragDirection();
        if(direction.y >= 1) OnDragDownToUp?.Invoke();
    }

    private Vector2 GetDragDirection()
    {
        Vector3 direction = _dragPreviousPosition - _dragStartPosition;
        direction.Normalize();
        
        return direction;
    }
}
