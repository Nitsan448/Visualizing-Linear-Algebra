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
		VectorsManager.VectorsUpdated += UpdateResultText;
	}

	private void OnDisable()
	{
		VectorsManager.VectorsUpdated -= UpdateResultText;
	}

	private void UpdateResultText()
	{
		if(Managers.Vectors.vectorOperation.operation == eVectorOperations.DotProduct)
		{
			_resultText.text = "Result = " + StringExtensions.FloatToString((float)Managers.Vectors.result);
		}
		else
		{
			_resultText.text = $"Result = " + StringExtensions.Vector3ToString((Vector3)Managers.Vectors.result);
		}
    }
}
