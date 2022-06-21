using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class ATransformUI: MonoBehaviour
{
	protected TMP_InputField _transformInput;

	private void OnEnable()
	{
		_transformInput = GetComponent<TMP_InputField>();
		UpdateTransformUI();
		UIManager.NumberOfDecimalsChanged += UpdateTransformUI;
	}

	// Update is called once per frame
	void OnDisable()
	{
		UIManager.NumberOfDecimalsChanged -= UpdateTransformUI;
	}

	public abstract void UpdateTransformUI();
}
