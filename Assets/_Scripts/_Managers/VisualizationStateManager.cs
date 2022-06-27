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
				UpdateObjectsEnabledState(_vectorOperationsObjects, true);
				UpdateObjectsEnabledState(_transformationsObjects, false);
				break;
			case eVisualizationState.MatrixTransformations:
				UpdateObjectsEnabledState(_transformationsObjects, true);
				UpdateObjectsEnabledState(_vectorOperationsObjects, false);
				break;
		}

		StateChanged?.Invoke();
	}

	private void UpdateObjectsEnabledState(List<GameObject> objectsToUpdate, bool enabled)
	{
		foreach (GameObject gameObject in objectsToUpdate)
		{
			gameObject.SetActive(enabled);
		}
	}
}
