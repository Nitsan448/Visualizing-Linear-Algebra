using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TransfromValueToManipulateDropDown : MonoBehaviour
{
    private TMP_Dropdown dropDown;
    [SerializeField] private ObjectTransformInput _objectTransfromInput;

    // Start is called before the first frame update
    void Start()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        dropDown.onValueChanged.AddListener(delegate { SetTranformationValueToManipulate(); });
        dropDown.value = 0;
    }



    public void SetTranformationValueToManipulate()
    {
        eTransformValue value = (eTransformValue)dropDown.value;
        Managers.Transformations.transformValueToManipulate = value;
        _objectTransfromInput.UpdateVectorUI();
    }
}
