using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorsDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _firstLine;
    [SerializeField] private LineRenderer _secondLine;
    [SerializeField] private LineRenderer _resultLine;
    [SerializeField] private LineRenderer _projectionShadow;

	private void OnEnable()
	{
        Managers.Vectors.VectorsUpdated += UpdateAllLines;
    }

	private void OnDisable()
	{
        Managers.Vectors.VectorsUpdated -= UpdateAllLines;
    }

    private void UpdateAllLines()
    {
        UpdateLine(_firstLine, Vector3.zero, Managers.Vectors.Vectors[0]);
        UpdateSecondLine();
        UpdateResultLine();
        UpdateProjectionShadow();
    }

    private void UpdateLine(LineRenderer line, Vector3 startPosition, Vector3 endPosition)
    {
        line.SetPosition(0, startPosition);
        line.SetPosition(1, endPosition);
    }

    private void UpdateSecondLine()
	{
        eVectorOperations operation = Managers.Vectors.VectorOperation.Operation;
        if (operation == eVectorOperations.Addition)
        {
            UpdateLine(_secondLine, Managers.Vectors.Vectors[0], (Vector3)Managers.Vectors.Result);
        }
        else
        {
            UpdateLine(_secondLine, Vector3.zero, Managers.Vectors.Vectors[1]);
        }
    }

    private void UpdateResultLine()
	{
        eVectorOperations operation = Managers.Vectors.VectorOperation.Operation;
        Vector3 result;
        if (operation == eVectorOperations.DotProduct)
		{
            UpdateLine(_resultLine, Vector3.zero, Vector3.zero);
        }
		else
		{
            result = (Vector3)Managers.Vectors.Result;
            UpdateLine(_resultLine, Vector3.zero, result);
        }
        UpdateResultLineWidth();
	}

    private void UpdateResultLineWidth()
	{
        if(Managers.Vectors.VectorOperation.Operation == eVectorOperations.Projection)
		{
            _resultLine.widthMultiplier = 1.5f;
        }
		else
		{
            _resultLine.widthMultiplier = 1;
        }
	}

    private void UpdateProjectionShadow()
	{
        eVectorOperations operation = Managers.Vectors.VectorOperation.Operation;
        if(operation == eVectorOperations.Projection)
		{
            _projectionShadow.gameObject.SetActive(true);
            UpdateLine(_projectionShadow, Managers.Vectors.Vectors[0], (Vector3)Managers.Vectors.Result);
		}
		else
		{
            _projectionShadow.gameObject.SetActive(false);
        }
	}
}
