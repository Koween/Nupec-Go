using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlanaResultsDocs : MonoBehaviour
{
    [SerializeField] private List<Sprite> _docs;
    [SerializeField] private Image _docImage;
    [SerializeField] private TextMeshProUGUI _pageIndex;
    [SerializeField] private Button _leftButton, _rightButton;

    private Animator _animator;
    [SerializeField] private int _docIndex;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _docImage = GetComponent<Image>();
        _pageIndex.text = "1/1";
        SetSideButtonsColors();
    }

    // Start is called before the first frame update
    public void UpdateDocumentIndex(int direction)
    {
        _docIndex = direction > 0? _docIndex + 1 : _docIndex - 1;
        if(_docIndex >= _docs.Count)
            _docIndex = 0;
        
        if(_docIndex < 0)
            _docIndex = _docs.Count - 1;

        _pageIndex.text = $"{_docIndex + 1}/{_docs.Count}";

        SetSideButtonsColors();
        _animator.Play("PlanaDocFadeOut");
    }

    private void SetSideButtonsColors()
    {
        if(_docIndex == _docs.Count - 1)
        _rightButton.interactable = false;
        else
        _rightButton.interactable = true;
        
        if(_docIndex == 0)
        _leftButton.interactable = false;
        else
        _leftButton.interactable = true;
    }

    public void LoadDocument()
    {
        _docImage.sprite = _docs[_docIndex];
        _animator.Play("PlanaDocFadeIn");
    }

}
