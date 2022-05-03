using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OperationsVector : MonoBehaviour
{
	[SerializeField] private int vectorIndex;

	private TMP_InputField _vectorInput;

	private void OnEnable()
	{
		_vectorInput = GetComponent<TMP_InputField>();
		VectorsManager.VectorsUpdated += UpdateVectorUI;
	}

	private void OnDisable()
	{
		VectorsManager.VectorsUpdated -= UpdateVectorUI;
	}

	private void Start()
	{
		GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { SetVector(); });
	}

	private void SetVector()
	{
		Managers.Vectors.SetVectorByString(vectorIndex, _vectorInput.text);
	}

	private void UpdateVectorUI()
	{
		Vector3 vector = Managers.Vectors.vectorByIndex[vectorIndex];
		string newVectorText = StringExtensions.Vector3ToString(vector);
		_vectorInput.text = newVectorText;
	}
}
