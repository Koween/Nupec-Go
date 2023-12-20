using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlanaResultsDocs : MonoBehaviour
{
    [SerializeField] private List<Sprite> _docs;
    [SerializeField] private Image _docImage;
    
    private Animator _animator;
    [SerializeField] private int _docIndex;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _docImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    public void UpdateDocumentIndex(int direction)
    {
        _docIndex = direction > 0? _docIndex + 1 : _docIndex - 1;
        if(_docIndex >= _docs.Count)
            _docIndex = 0;
        
        if(_docIndex < 0)
            _docIndex = _docs.Count - 1;
        _animator.Play("PlanaDocFadeOut");
    }


    public void LoadDocument()
    {
        _docImage.sprite = _docs[_docIndex];
        _animator.Play("PlanaDocFadeIn");
    }

}
