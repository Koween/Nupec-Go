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

    [SerializeField] private TextMeshProUGUI _titleCase;
    [SerializeField] private TextMeshProUGUI _descriptionCase;

    private GameObject _currentCase;
    private int _caseIndex = 0;

    private void Awake()
    {
        _currentCase = _clinicalCasesContainer.GetChild(0).gameObject;
        SetCurrentCase();
    }

    //Set the current case as completed and configure the next one.
    public void UpdateCurrentCase()
    {
        SetCaseCompleted();
        _caseIndex ++;
        SetCurrentCase();
        
    }

    //configure the current case images and text
    private void SetCurrentCase()
    {
        _currentCase = _clinicalCasesContainer.GetChild(_caseIndex).gameObject;
        _currentCase.transform.GetChild(_caseIndex).GetComponent<Image>().color = _currentBGColor;
        _currentCase.transform.GetChild(_caseIndex).GetChild(_caseIndex).GetComponent<Image>().color = new Color(0,0,0,0);
        setCurrentCaseDescription();
    }

    private void SetCaseCompleted()
    {
        _currentCase.transform.GetChild(_caseIndex).GetComponent<Image>().color = _completeBGColor;
        _currentCase.transform.GetChild(_caseIndex).GetChild(_caseIndex).GetComponent<Image>().color = new Color(1,1,1,1);
    }

    private void setCurrentCaseDescription()
    {
        _titleCase.text = _casesDataText[_caseIndex].Title;
        _descriptionCase.text = _casesDataText[_caseIndex].Description;
    }
    
}
