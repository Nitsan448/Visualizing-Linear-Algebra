using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberOfDecimalsDropdown : MonoBehaviour
{
    void Start()
    {
        TMP_Dropdown dropDown = GetComponent<TMP_Dropdown>();
        dropDown.onValueChanged.AddListener(delegate { Managers.UI.ChangeNumberOfDecimals(dropDown.value); });
    }
}
