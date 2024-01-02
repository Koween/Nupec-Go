using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenToWorldRaycast : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _collisionCordinates;
    private UnityEvent<GameObject> _onRaycastCollision;
    private UnityEvent _onRaycastCollisionFail;
    private UnityEvent<Vector3> _onRaycastCollisionPointChange;

    void Start()
    {
        _camera = Camera.main;
    }

    public bool ThrowRayScreenToWorld(Vector3 screenPosition,LayerMask collisionLayerMask)
    {
        Ray worldPoint = _camera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(worldPoint, out RaycastHit raycastHit, 1000, collisionLayerMask))
        {
            if(raycastHit.collider.GetComponent<InteractiveObject>().InteractionEnable)
            {
                _collisionCordinates = raycastHit.point;
                return true;
            }
        }
        return false;
    }
}
