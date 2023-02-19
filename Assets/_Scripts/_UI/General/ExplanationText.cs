using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplanationText : MonoBehaviour
{
    private TextMeshProUGUI _explanationText;

    void Awake()
    {
        _explanationText = GetComponent<TextMeshProUGUI>();
    }

	private void OnEnable()
	{
		Managers.VisualizationState.StateChanged += UpdateTextToNewState;
		Managers.Vectors.OperationChanged += UpdateTextToNewOperation;
	}

	private void OnDisable()
	{
		Managers.VisualizationState.StateChanged -= UpdateTextToNewState;
		Managers.Vectors.OperationChanged -= UpdateTextToNewOperation;
	}

	private void UpdateTextToNewState()
	{
		switch (Managers.VisualizationState.State)
		{
			case eVisualizationState.VectorOperations:
				UpdateTextToNewOperation();
				break;
			case eVisualizationState.MatrixTransformations:
				_explanationText.text = Explanations.MatrixTransformationExplanation;
				break;
		}
	}

	private void UpdateTextToNewOperation()
	{
		eVectorOperations operation = Managers.Vectors.VectorOperation.Operation;
		_explanationText.text = Explanations.ExplanationByVectorOperation[operation];
	}
}
