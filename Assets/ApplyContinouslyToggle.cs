using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyContinouslyToggle : MonoBehaviour
{
    void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.isOn = Managers.Transformations.ApplyContinuously;
        toggle.onValueChanged.AddListener(delegate { Managers.Transformations.UpdateApplyContinouslyValue(toggle.isOn); });
    }
}
