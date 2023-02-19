using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontControlAxisToggles : AToggleGroup
{
	protected override int GetStartingToggleIndex()
	{
		return (int)Managers.Vectors.VectorMouseController.DontControlAxis;
	}

	protected override void OnToggleValueChanged(Toggle toggle, int toggleIndex)
	{
		if (toggle.isOn)
		{
            Managers.Vectors.VectorMouseController.DontControlAxis = (eAxes)toggleIndex;
		}
		if (!_toggleGroup.AnyTogglesOn())
		{
			Managers.Vectors.VectorMouseController.DontControlAxis = eAxes.None;
		}
	}
}
