using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontControlAxisToggles : MonoBehaviour
{
    [SerializeField] private List<Toggle> _toggles;

    private ToggleGroup _toggleGroup;

    // Start is called before the first frame update
    void Start()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
        for(int i = 0; i < _toggles.Count; i++)
		{
			int temp = i;
			_toggles[i].onValueChanged.AddListener(delegate { OnToggleValueChanged(_toggles[temp], temp); });
		}
    }

    private void OnToggleValueChanged(Toggle toggle, int toggleIndex)
	{
		Debug.Log(toggleIndex);
		if (toggle.isOn)
		{
            Managers.Vectors.VectorMouseController.DontControlAxis = (eAxes)toggleIndex;
		}
		if (!_toggleGroup.AnyTogglesOn())
		{
			Managers.Vectors.VectorMouseController.DontControlAxis = eAxes.None;
		}
		Debug.Log(Managers.Vectors.VectorMouseController.DontControlAxis);
	}
}
