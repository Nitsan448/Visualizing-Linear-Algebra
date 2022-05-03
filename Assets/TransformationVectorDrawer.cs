using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationVectorDrawer : MonoBehaviour
{
    [SerializeField] private float _startWidth = 0.03f;
    [SerializeField] private float _endWidth = 0.015f;
    private LineRenderer _line;

    private void OnEnable()
    {
        _line = GetComponent<LineRenderer>();
        _line.startWidth = _startWidth;
        _line.endWidth = _endWidth;
        TransformationsManager.TransformationApplied += UpdateLine;
    }

    private void Start()
    {
        //Remove and fix source problem (script execution order)
        UpdateLine();
    }

    private void OnDisable()
    {
        TransformationsManager.TransformationApplied -= UpdateLine;
    }

    void UpdateLine()
    {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Managers.Transformations.ObjectToTransform.position);
    }
}
