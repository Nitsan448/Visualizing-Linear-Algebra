using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class VisualizationStateManager: MonoBehaviour, IGameManager
{
	public eVisualizationState State { get; private set; }
	public eManagerStatus Status { get; private set; }

	public event Action StateChanged;

	[SerializeField] private List<GameObject> _vectorOperationsObjects;

	[SerializeField] private List<GameObject> _transformationsObjects;

	[SerializeField] private List<GameObject> _spanObjects;

	[SerializeField] private eVisualizationState _startingState;

	public void Startup()
	{
		Status = eManagerStatus.Initializing;
		SetState(_startingState);
		Status = eManagerStatus.Started;
	}

	public void SetState(eVisualizationState newState)
	{
		State = newState;

		switch (State)
		{
			case eVisualizationState.VectorOperations:
				SetVectorOperationsState();
				break;
			case eVisualizationState.MatrixTransformations:
				SetTransformationsState();
				break;
			case eVisualizationState.Span:
				SetSpanState();
				break;
		}

		StateChanged?.Invoke();
	}

	private void SetVectorOperationsState()
	{
		UpdateObjectsEnabledState(_vectorOperationsObjects, true);
		UpdateObjectsEnabledState(_transformationsObjects, false);
		UpdateObjectsEnabledState(_spanObjects, false);
	}

	private void SetTransformationsState()
	{
		UpdateObjectsEnabledState(_transformationsObjects, true);
		UpdateObjectsEnabledState(_vectorOperationsObjects, false);
		UpdateObjectsEnabledState(_spanObjects, false);
	}

	private void SetSpanState()
	{
		UpdateObjectsEnabledState(_spanObjects, true);
		UpdateObjectsEnabledState(_vectorOperationsObjects, false);
		UpdateObjectsEnabledState(_transformationsObjects, false);
	}

	private void UpdateObjectsEnabledState(List<GameObject> objectsToUpdate, bool enabled)
	{
		foreach (GameObject gameObject in objectsToUpdate)
		{
			gameObject.SetActive(enabled);
		}
	}
}
