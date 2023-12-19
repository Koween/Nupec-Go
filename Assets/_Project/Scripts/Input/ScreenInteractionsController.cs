using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScreenInteractionsController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _collisionLayerMask;
    private Vector3 collisionCordinates;
    [SerializeField] private UnityEvent<GameObject> _onRaycastCollision, _onRaycastCollisionFail;
    [SerializeField] private UnityEvent<Vector3> _onRaycastCollisionPointChange;
    
    private Vector3 dragStartPos;
    private Vector3 dragEndPos;
    private UnityEvent<Vector2> _onSwipe; 

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragEndPos = eventData.position;
        Vector3 direction = dragEndPos - dragStartPos;
        direction.Normalize();
        _onSwipe?.Invoke(direction);
    }
   
   public void ThrowRayScreenToWorld(Vector3 screenPosition)
    {
        if(!gameObject.activeSelf) return;
        Ray worldPoint = _camera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(worldPoint, out RaycastHit raycastHit, 1000, _collisionLayerMask))
        {
            collisionCordinates = raycastHit.point;
            _onRaycastCollisionPointChange?.Invoke(collisionCordinates);
            _onRaycastCollision?.Invoke(raycastHit.collider.gameObject);
        }
        else
            _onRaycastCollisionFail?.Invoke(null);
    }
}
