using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class ATransformUI: MonoBehaviour
{
	protected TMP_InputField _transformInput;

	protected virtual void OnEnable()
	{
		_transformInput = GetComponent<TMP_InputField>();
		Managers.UI.NumberOfDecimalsChanged += UpdateTransformUI;
	}

	// Update is called once per frame
	protected virtual void OnDisable()
	{
		Managers.UI.NumberOfDecimalsChanged -= UpdateTransformUI;
	}

	public abstract void UpdateTransformUI();
}
