using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using System;

//This sript get the index that identifies when a option into a dropdown has been pressed
//This script must be attached in the item template of a dropdown menu
public class customDropDownButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent<int> _onSelect;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(eventData.selectedObject.transform.GetSiblingIndex() - 2);
        _onSelect?.Invoke(eventData.selectedObject.transform.GetSiblingIndex() - 2);
    }
}
