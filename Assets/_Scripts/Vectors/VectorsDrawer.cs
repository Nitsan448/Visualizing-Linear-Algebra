using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorsDrawer : MonoBehaviour
{
    [SerializeField] private float _startWidth = 0.03f;
    [SerializeField] private float _endWidth = 0.015f;
    
    [SerializeField] private LineRenderer _firstLine;
    [SerializeField] private LineRenderer _secondLine;
    [SerializeField] private LineRenderer _resultLine;

	private void OnEnable()
	{
        VectorsManager.VectorsUpdated += UpdateAllLines;
    }

	private void Start()
	{
        //Remove and fix source problem (script execution order)
        _firstLine.startWidth = _startWidth;
        _firstLine.endWidth = _endWidth;
        _secondLine.startWidth = _startWidth;
        _secondLine.endWidth = _endWidth;
        _resultLine.startWidth = _startWidth;
        _resultLine.endWidth = _endWidth;
        UpdateAllLines();
	}

	private void OnDisable()
	{
        VectorsManager.VectorsUpdated -= UpdateAllLines;
    }

	private void UpdateAllLines()
    {
        UpdateLine(_firstLine, Vector3.zero, Managers.Vectors.vectorByIndex[1]);
        UpdateLine(_secondLine, Vector3.zero, Managers.Vectors.vectorByIndex[2]);

        Vector3 result;
        if (Managers.Vectors.result.GetType() == typeof(Vector3))
        {
            result = (Vector3)Managers.Vectors.result;
            UpdateLine(_resultLine, Vector3.zero, result);
        }
        else
        {
            UpdateLine(_resultLine, Vector3.zero, Vector3.zero);
        }
    }

    void UpdateLine(LineRenderer line, Vector3 startPosition, Vector3 endPosition)
    {
        line.SetPosition(0, startPosition);
        line.SetPosition(1, endPosition);
    }
}
