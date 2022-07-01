using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class OperationsVector : MonoBehaviour
{
	[SerializeField] private int vectorIndex;
	protected TMP_InputField _vectorInput;

	private void Awake()
	{
		_vectorInput = GetComponent<TMP_InputField>();
		_vectorInput.onEndEdit.AddListener(delegate { SetVector(); });
	}

	private void OnEnable()
	{
		Managers.Vectors.VectorsUpdated += UpdateVectorUI;
		UpdateVectorUI();
	}

	private void OnDisable()
	{
		Managers.Vectors.VectorsUpdated -= UpdateVectorUI;
	}

	private void SetVector()
	{
		Managers.Vectors.SetVectorByString(vectorIndex, _vectorInput.text);
	}

	private void UpdateVectorUI()
	{
		Vector3 vector = Managers.Vectors.Vectors[vectorIndex];
		_vectorInput.text = StringExtensions.Vector3ToString(vector);
		_vectorInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
	}
}
