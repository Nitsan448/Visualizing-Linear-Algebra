using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommonTransformationDropdown : MonoBehaviour
{
    void Start()
    {
        TMP_Dropdown dropDown = GetComponent<TMP_Dropdown>();
        dropDown.onValueChanged.AddListener(delegate { Managers.Transformations.ChangeToCommonMatrix(dropDown.value); });
    }
}