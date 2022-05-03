using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector4UI : AVectorUI
{
	public override void UpdateVectorUI()
	{
		_vectorInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
		_vectorInput.text = StringExtensions.UpdateNumberOfDecimalsShownVector4(_vectorInput.text);
	}
}
