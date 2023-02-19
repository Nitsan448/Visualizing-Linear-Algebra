using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlVectorToggleGroup : AToggleGroup
{
	protected override int GetStartingToggleIndex()
	{
		return Managers.Vectors.VectorMouseController.ControlledVectorIndex;
	}

	protected override void OnToggleValueChanged(Toggle toggle, int toggleIndex)
	{
		if (toggle.isOn)
		{
			Managers.Vectors.VectorMouseController.ControlledVectorIndex = toggleIndex;
		}
	}
}
