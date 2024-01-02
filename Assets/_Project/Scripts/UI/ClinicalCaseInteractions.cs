using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClinicalCaseInteractions : MonoBehaviour
{
    [SerializeField] private GameObject _sandbox, _plate, _cat;
    [SerializeField] private Transform _sandboxReferencePosition, _plateReferencePosition;
    private GameObject _plateInstance, _sandboxInstance;
    private Vector3 _defaultPosition;

    public void SpawnSandBox()
    {
        _sandboxInstance = Instantiate(_sandbox, _sandbox.transform.position, _sandbox.transform.rotation);
    }

    public void SpawnPlate()
    {
        _plateInstance = Instantiate(_plate, _plate.transform.position, _plate.transform.rotation);
    }

    public void DestroyItems()
    {
        Destroy(_plateInstance);
        Destroy(_sandboxInstance);
    }

    public void SetCatOnDefaultPosition()
    {
        _cat.transform.position = _defaultPosition;
    }

    public void SetCatOnSandboxPosition()
    {
        _cat.transform.position = _sandboxReferencePosition.position;
    }

    public void SetCatOnPlatePosition()
    {
        _cat.transform.position = _plateReferencePosition.position;
    }
 }
