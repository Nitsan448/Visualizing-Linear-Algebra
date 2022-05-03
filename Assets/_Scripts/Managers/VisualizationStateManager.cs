using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualizationStateManager: MonoBehaviour, IGameManager
{
	public eManagerStatus status { get; private set; }
	public static eVisualizationState VisualizationState { get; private set; }

	[SerializeField] private GameObject _vectorOperationsUI;
	[SerializeField] private GameObject _vectorOperationsObjects;

	[SerializeField] private GameObject _transformationsUI;
	[SerializeField] private GameObject _transformationsObjects;

	[SerializeField] private eVisualizationState _startingVisualizationState;

	[SerializeField] private TextMeshProUGUI _explanationText;


	public void Startup()
	{
		status = eManagerStatus.Initializing;
		SetVisualizationState(_startingVisualizationState);
		status = eManagerStatus.Started;
	}

	public void SetVisualizationState(eVisualizationState newState)
	{
		VisualizationState = newState;

		switch (VisualizationState)
		{
			case eVisualizationState.VectorOperations:
				UpdateVectorOperationsEnabledState(true);
				UpdateTransformationsEnabledState(false);
				_explanationText.text = Explanations.ExplanationByVectorOperation[Managers.Vectors.vectorOperation.operation];
				break;
			case eVisualizationState.MatrixTransformations:
				UpdateTransformationsEnabledState(true);
				UpdateVectorOperationsEnabledState(false);
				_explanationText.text = Explanations.MatrixTransformationExplanation;
				break;
		}
	}

	private void UpdateVectorOperationsEnabledState(bool enabled)
	{
		_vectorOperationsUI.SetActive(enabled);
		_vectorOperationsObjects.SetActive(enabled);
	}

	private void UpdateTransformationsEnabledState(bool enabled)
	{
		_transformationsUI.SetActive(enabled);
		_transformationsObjects.SetActive(enabled);
	}
}
