using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisToggle : MonoBehaviour
{
    [SerializeField] private GameObject _axis;
    private Toggle _toggle;
    // Start is called before the first frame update
    void Start()
    {
        _toggle = GetComponent<Toggle>();
        UpdateAxisState();
        _toggle.onValueChanged.AddListener(delegate { UpdateAxisState(); });
    }

    private void UpdateAxisState()
	{
        _axis.SetActive(_toggle.isOn);
    }
}
