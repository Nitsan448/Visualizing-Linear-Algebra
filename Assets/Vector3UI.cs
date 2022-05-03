using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3UI : AVectorUI
{
	public override void UpdateVectorUI()
	{
		_vectorInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
		_vectorInput.text = StringExtensions.UpdateNumberOfDecimalsShownVector3(_vectorInput.text);
	}
}