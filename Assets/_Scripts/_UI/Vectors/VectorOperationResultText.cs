using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VectorOperationResultText : MonoBehaviour
{
    private TextMeshProUGUI _resultText;

	private void OnEnable()
	{
		_resultText = GetComponent<TextMeshProUGUI>();
		Managers.Vectors.VectorsUpdated += UpdateResultText;
	}

	private void OnDisable()
	{
		Managers.Vectors.VectorsUpdated -= UpdateResultText;
	}

	private void UpdateResultText()
	{
		if(Managers.Vectors.VectorOperation.operation == eVectorOperations.DotProduct)
		{
			_resultText.text = $"Result:\n{StringExtensions.FloatToString((float)Managers.Vectors.Result)}";
		}
		else
		{
			_resultText.text = $"Result:\n{StringExtensions.Vector3ToString((Vector3)Managers.Vectors.Result)}";
		}
    }
}
