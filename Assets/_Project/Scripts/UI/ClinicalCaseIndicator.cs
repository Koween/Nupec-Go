using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class ClinicalCaseIndicator : MonoBehaviour
{

    [Serializable]
    private struct CaseDataText
    {
        public string Title;
        [TextArea] public string Description;
    }

    [SerializeField] private List<CaseDataText> _casesDataText = new List<CaseDataText>();
    [SerializeField] private Color _completeBGColor;
    [SerializeField] private Color _currentBGColor;
    [SerializeField] private Transform _clinicalCasesContainer;
    [SerializeField] private Sprite _checkIcon;
    [SerializeField] private TextMeshProUGUI _titleCase;
    [SerializeField] private TextMeshProUGUI _descriptionCase;

    private GameObject _currentCase;
    [SerializeField] private int _caseIndex = 0;

    private void Awake()
    {
        _currentCase = _clinicalCasesContainer.GetChild(0).gameObject;
        SetCurrentCase();
    }

    //Set the current case as completed and configure the next one.
    public void UpdateCurrentCase()
    {
        SetCaseCompleted();
        _caseIndex += 1;
        SetCurrentCase();
        
    }

    //configure the current case images and text
    private void SetCurrentCase()
    {
        _currentCase = _clinicalCasesContainer.GetChild(_caseIndex).gameObject;
        _currentCase.transform.GetChild(0).GetComponent<Image>().color = _currentBGColor;
        _currentCase.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0,0,0,0);
        setCurrentCaseDescription();
    }

    //configure the last case images and text as completed
    private void SetCaseCompleted()
    {
        _currentCase.transform.GetChild(0).GetComponent<Image>().color = _completeBGColor;
        _currentCase.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _checkIcon;
        _currentCase.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1,1,1,1);
    }

    private void setCurrentCaseDescription()
    {
        _titleCase.text = _casesDataText[_caseIndex].Title;
        _descriptionCase.text = _casesDataText[_caseIndex].Description;
    }
    
}
