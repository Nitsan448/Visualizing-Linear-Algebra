using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformationInputToggleGroup : AToggleGroup
{
	[SerializeField] private GameObject MatrixInput;
	[SerializeField] private GameObject FunctionInput;

	protected override int GetStartingToggleIndex()
	{
		return 0;
	}

	protected override void OnToggleValueChanged(Toggle toggle, int toggleIndex)
	{
		if(toggleIndex == 0)
		{
			MatrixInput.SetActive(toggle.isOn);
			Managers.Transformations.UpdateMatrix
			(StringExtensions.LinearTransformationStringToMatrix(FunctionInput.GetComponentInChildren<TMPro.TMP_InputField>().text));
			FunctionInput.SetActive(!toggle.isOn);
		}
		else
		{
			FunctionInput.SetActive(toggle.isOn);
			MatrixInput.SetActive(!toggle.isOn);
		}
	}
}
