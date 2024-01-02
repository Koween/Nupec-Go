using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownFixIndex : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropDown;
    private int _currentDropDownSelectedValue;

    public void Awake()
    {
        _dropDown.value = -1;
    }

    //this method must be subscribed to the _onSelect event of the customsDropDownButton script
    public void OnChangeDropDownOption(int value)
    {
        _currentDropDownSelectedValue = value;
    }

    //Ensures that the dropdown value is the same that the user has chosed.
    public void Update()
    {
        if(_dropDown.value != _currentDropDownSelectedValue && _dropDown.value != -1)
        {
            _dropDown.value = _currentDropDownSelectedValue;
        }
    }
}
