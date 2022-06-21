using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplanationText : MonoBehaviour
{
    private TextMeshProUGUI _explanationText;

    void Start()
    {
        _explanationText = GetComponent<TextMeshProUGUI>();
    }

	private void OnEnable()
	{
		Managers.VisualizationState.VisualizationStateChanged += UpdateExplanationTextToNewState;
	}

	private void OnDisable()
	{
		Managers.VisualizationState.VisualizationStateChanged -= UpdateExplanationTextToNewState;
	}

	private void UpdateExplanationTextToNewState(eVisualizationState newState)
	{
		switch (newState)
		{
			case eVisualizationState.VectorOperations:
				_explanationText.text = Explanations.ExplanationByVectorOperation[Managers.Vectors.vectorOperation.operation];
				break;
			case eVisualizationState.MatrixTransformations:
				_explanationText.text = Explanations.MatrixTransformationExplanation;
				break;
		}
	}
}
