using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScreenToWorldRaycast))]
public class ScreenInteractions : MonoBehaviour, 
IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    
    private Vector2 _dragStartPosition;
    private Vector2 _dragPreviousPosition;
    private Vector2 _lastDragDirection;
    public UnityEvent OnSwipe;


    [SerializeField] private bool _beingDrag;
    [SerializeField] private bool _beingDragUp;
    [SerializeField] private bool _beingTouch;


    public UnityEvent<float> OnTouchEvent, OnChnageDragDirectionEvent, OnDragDownToUp;
    public static ScreenInteractions Instance;
    private ScreenToWorldRaycast _screenToWorldRaycast;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _interactionElapsedTime;

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

    private void Update()
    {
        if(_beingTouch && !_beingDrag)
        {
            _interactionElapsedTime += Time.deltaTime;
            OnTouchEvent?.Invoke(_interactionElapsedTime);
        }
        else
            _beingTouch = false;

        if(_beingDrag)
        {
            _interactionElapsedTime += Time.deltaTime;
        }

        if(_beingDragUp)
        {
            _interactionElapsedTime += Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_screenToWorldRaycast.ThrowRayScreenToWorld(eventData.position, layerMask))
        {
            
            _beingTouch = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_beingTouch)
        {
            _beingTouch = false;
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragStartPosition = eventData.position;
        _dragPreviousPosition = _dragStartPosition;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.position != _dragPreviousPosition)
        {
            _beingDrag = true;
            _dragPreviousPosition = eventData.position;
        }

        else
            _beingDrag = false;

        var currentDirection = GetDragDirection();
        if(_lastDragDirection != currentDirection)
        {
            _lastDragDirection = currentDirection;
            OnChnageDragDirectionEvent?.Invoke(_interactionElapsedTime);
        }

        if(currentDirection.y > 0 && _beingDrag)
        {
            OnDragDownToUp?.Invoke(_interactionElapsedTime);
            _beingDragUp = true;
        }
        else
            _beingDragUp = false;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _beingDrag = false;
        _beingDragUp = false;
        OnSwipe?.Invoke();
        
    }

    private Vector2 GetDragDirection()
    {
        Vector3 direction = _dragPreviousPosition - _dragStartPosition;
        direction.Normalize();
        
        return direction;
    }
}
