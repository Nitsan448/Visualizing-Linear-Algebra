using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class AVectorUI: MonoBehaviour
{
	protected TMP_InputField _vectorInput;

	private void OnEnable()
	{
		_vectorInput = GetComponent<TMP_InputField>();
		UpdateVectorUI();
		UIManager.NumberOfDecimalsChanged += UpdateVectorUI;
	}

	// Update is called once per frame
	void OnDisable()
	{
		UIManager.NumberOfDecimalsChanged -= UpdateVectorUI;
	}

	public abstract void UpdateVectorUI();
}
