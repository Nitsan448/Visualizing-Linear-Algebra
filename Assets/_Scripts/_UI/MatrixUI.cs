using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixUI : ATransformUI
{
	public override void UpdateTransformUI()
	{
		_transformInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
		_transformInput.text = StringExtensions.UpdateNumberOfDecimalsShownVector4(_transformInput.text);
	}
}
