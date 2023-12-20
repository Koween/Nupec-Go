using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipMessage : MonoBehaviour
{
    [SerializeField] private TextMeshPro _messageText;

    private void Start()
    {
        _messageText.text = "";
    }

    public void SetMessage(string text)
    {
        _messageText.text = text;
    }
}
