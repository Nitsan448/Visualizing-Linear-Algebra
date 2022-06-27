using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class VisualizationStateManager: MonoBehaviour, IGameManager
{
	public static eVisualizationState VisualizationState { get; private set; }
	public eManagerStatus Status { get; private set; }

	public event Action<eVisualizationState> VisualizationStateChanged;

	[SerializeField] private List<GameObject> _vectorOperationsObjects;

	[SerializeField] private List<GameObject> _transformationsObjects;

	[SerializeField] private eVisualizationState _startingVisualizationState;

	public void Startup()
	{
		Status = eManagerStatus.Initializing;
		SetVisualizationState(_startingVisualizationState);
		Status = eManagerStatus.Started;
	}

	public void SetVisualizationState(eVisualizationState newState)
	{
		VisualizationState = newState;

		switch (VisualizationState)
		{
			case eVisualizationState.VectorOperations:
				UpdateObjectsEnabledState(_vectorOperationsObjects, true);
				UpdateObjectsEnabledState(_transformationsObjects, false);
				break;
			case eVisualizationState.MatrixTransformations:
				UpdateObjectsEnabledState(_transformationsObjects, true);
				UpdateObjectsEnabledState(_vectorOperationsObjects, false);
				break;
		}

		OnVisualizationStateChanged();
	}

	private void UpdateObjectsEnabledState(List<GameObject> objectsToUpdate, bool enabled)
	{
		foreach (GameObject gameObject in objectsToUpdate)
		{
			gameObject.SetActive(enabled);
		}
	}

	private void OnVisualizationStateChanged()
	{
		VisualizationStateChanged?.Invoke(VisualizationState);
	}
}
