using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OperationsVector : ATransformUI
{
	[SerializeField] private int vectorIndex;

	private void OnEnable()
	{
		_transformInput = GetComponent<TMP_InputField>();
		VectorsManager.VectorsUpdated += UpdateTransformUI;
	}

	private void OnDisable()
	{
		VectorsManager.VectorsUpdated -= UpdateTransformUI;
	}

	private void Start()
	{
		GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { SetVector(); });
	}

	private void SetVector()
	{
		Managers.Vectors.SetVectorByString(vectorIndex, _transformInput.text);
	}

	public override void UpdateTransformUI()
	{
		Vector3 vector = Managers.Vectors.Vectors[vectorIndex];
		string newVectorText = StringExtensions.Vector3ToString(vector);
		_transformInput.text = StringExtensions.UpdateNumberOfDecimalsShownVector4(_transformInput.text);
		_transformInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
	}
}
