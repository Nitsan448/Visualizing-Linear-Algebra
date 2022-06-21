using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3UI : ATransformUI
{
	public override void UpdateTransformUI()
	{
		_transformInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
		_transformInput.text = StringExtensions.UpdateNumberOfDecimalsShownVector3(_transformInput.text);
	}
}